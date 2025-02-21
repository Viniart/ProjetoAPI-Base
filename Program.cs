using Microsoft.EntityFrameworkCore;
using ProjetoAPI.Context;
using ProjetoAPI.Endpoints;
using ProjetoAPI.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = "Data Source=produtos.db";
builder.Services.AddSqlite<ProdutosDbContext>(connectionString);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapProdutosEndpoints();

app.UseHttpsRedirection();

app.Run();