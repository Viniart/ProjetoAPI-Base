using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProjetoAPI.Context;
using ProjetoAPI.Endpoints;
using ProjetoAPI.Model;
using ProjetoAPI.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Produtos",
        Version = "v1",
        Description = "API do Projeto Produtos para aprendizado de desenvolvimento Backend"
    });
});

var connectionString = "Data Source=produtos.db";
builder.Services.AddSqlite<ProdutosDbContext>(connectionString);

// Validação
builder.Services.AddValidatorsFromAssemblyContaining<ProdutoValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Projetos v1");
    });
}

app.MapProdutosEndpoints();

app.UseHttpsRedirection();

app.Run();