using FluentValidation;
using System.Net;
using System.Text.Json;
using Usuarios.API.Erros;
using Usuarios.Domain.ExceptionsBase;

namespace Usuarios.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";

            // Define o status apropriado
            context.Response.StatusCode = ex switch
            {
                ValidationException _ => (int)HttpStatusCode.BadRequest,
                ErrorOnValidationException _ => (int)HttpStatusCode.BadRequest,
                InvalidOperationException _ => (int)HttpStatusCode.BadRequest,
                KeyNotFoundException _ => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };

            // Cria o objeto de resposta padronizado
            object response = ex switch
            {
                ValidationException ve => new
                {
                    statusCode = context.Response.StatusCode,
                    message = "Erro de validação.",
                    errors = ve.Errors.Select(e => e.ErrorMessage)
                },
                ErrorOnValidationException ce => new
                {
                    statusCode = context.Response.StatusCode,
                    message = "Erro de validação.",
                    errors = ce.ErrorMessages
                },
                InvalidOperationException ioe => new
                {
                    statusCode = context.Response.StatusCode,
                    message = ioe.Message
                },
                _ => new
                {
                    statusCode = context.Response.StatusCode,
                    message = _env.IsDevelopment() ? ex.Message : "Ocorreu um erro interno no servidor.",
                    stackTrace = _env.IsDevelopment() ? ex.StackTrace?.ToString() : null
                }
            };

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(json);
        }
    }
}