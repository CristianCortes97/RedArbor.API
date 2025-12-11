using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RedArbor.Domain.Interface;
using RedArbor.Domain.Interfaces;
using RedArbor.Infraestructure.Context;
using RedArbor.Infraestructure.Factories;
using RedArbor.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<DbredArborContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Configurar AutoMapper
builder.Services.AddAutoMapper(typeof(RedArbor.Application.Mappings.MappingProfile));

// Registrar Database Connection Factory para Dapper
builder.Services.AddSingleton<IDatabaseConnectionFactory, DatabaseConnectionFactory>();

// Registrar Repositorios
builder.Services.AddScoped<IEmployesRepository, EmployeeRepository>();

// Registrar Commands
builder.Services.AddScoped<RedArbor.Application.Commands.CreateEmployeeCommand>();
builder.Services.AddScoped<RedArbor.Application.Commands.UpdateEmployeeCommand>();
builder.Services.AddScoped<RedArbor.Application.Commands.DeleteEmployeeCommand>();

//// Registrar Queries
//builder.Services.AddScoped<RedArbor.Application.Queries.GetAllEmployeesQuery>();
//builder.Services.AddScoped<RedArbor.Application.Queries.GetEmployeeByIdQuery>();


// Registrar FluentValidation
builder.Services.AddScoped<RedArbor.Application.Validators.CreateEmployeeValidator>();
builder.Services.AddScoped<RedArbor.Application.Validators.UpdateEmployeeValidator>();


// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API RedArbor",
        Version = "8.0.0",
        Description = "Api para readarbor con un crud completo"
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
