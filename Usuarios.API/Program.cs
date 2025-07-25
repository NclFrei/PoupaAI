using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Usuarios.Application.Mapper;
using Usuarios.Application.Services;
using Usuarios.Domain.Interfaces;
using Usuarios.Infrastructure.Data;
using Usuarios.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UsuariosContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("MySqlConnection")
    )
);

// Add services to the container.
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(UsuarioProfile).Assembly);
});
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
