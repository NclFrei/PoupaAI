using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Usuarios.Domain.DTOs.Response;
using Usuarios.Domain.ExceptionsBase;

namespace Usuarios.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
       if (context.Exception is UsuarioException)
       {
            HandleProjectException(context);
       }
       else
       {
            ThrowUnknowException(context);
        }
    }

    private void HandleProjectException(ExceptionContext context)
    {
        var exception = context.Exception as ErrorOnValidationException;

        if (context.Exception is ErrorOnValidationException)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception.ErrorMessages));
        }
    }

    private void ThrowUnknowException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson("Erro desconhecido"));
    }
}
