﻿using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProjetoAPI.Context;
using ProjetoAPI.DTO;
using ProjetoAPI.Model;
using System.ComponentModel.DataAnnotations;

namespace ProjetoAPI.Endpoints
{
    public static class ProdutosEndpoints
    {
        public static void MapProdutosEndpoints(this WebApplication app)
        {
            app.MapGet("/produtos", async (ProdutosDbContext db) =>
            await db.Produtos.Include(p => p.Categoria).ToListAsync());

            app.MapGet("/produtos/{id}", async (Guid id, ProdutosDbContext db) =>
                await db.Produtos.FindAsync(id)
                    is Produto produto
                        ? Results.Ok(produto)
                        : Results.NotFound()
            )
            .Produces<Produto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            app.MapPost("/produtos", async (ProdutoDto dto, IValidator<Produto> validator, ProdutosDbContext db) =>
            {
                if (dto != null)
                {
                    var produto = new Produto()
                    {
                        Nome = dto.Nome,
                        Valor = dto.Valor,
                        Descricao = dto.Descricao,
                        CategoriaId = dto.CategoriaId
                    };

                    var validation = await validator.ValidateAsync(produto);

                    if (!validation.IsValid) return Results.ValidationProblem(validation.ToDictionary());

                    db.Produtos.Add(produto);
                    await db.SaveChangesAsync();

                    return Results.Created($"/produtos/{produto.Id}", produto);
                }
                else
                {
                    return Results.BadRequest("Requisição Inválida!");
                }
            })
            .Produces(StatusCodes.Status400BadRequest)
            .Produces<Produto>(StatusCodes.Status201Created);

            app.MapPut("/produtos/{id}", async (Guid id, ProdutoDto dto, IValidator<Produto> validator, ProdutosDbContext db) =>
            {
                var produto = new Produto()
                {
                    Nome = dto.Nome,
                    Valor = dto.Valor,
                    Descricao = dto.Descricao,
                    CategoriaId = dto.CategoriaId
                };

                var validation = await validator.ValidateAsync(produto);

                if (!validation.IsValid) return Results.ValidationProblem(validation.ToDictionary());

                var produtoEncontrado = await db.Produtos.FindAsync(id);

                if (produtoEncontrado is null) return Results.NotFound();

                produtoEncontrado.Nome = produto.Nome;
                produtoEncontrado.Valor = produto.Valor;
                produtoEncontrado.Descricao = produto.Descricao;
                produtoEncontrado.CategoriaId = produto.CategoriaId;

                await db.SaveChangesAsync();

                return Results.NoContent();
            })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

            app.MapDelete("/produtos/{id}", async (Guid id, ProdutosDbContext db) =>
            {
                var produtoEncontrado = await db.Produtos.FindAsync(id);

                if (produtoEncontrado is null) return Results.NotFound();

                db.Produtos.Remove(produtoEncontrado);
                await db.SaveChangesAsync();
                return Results.Ok(produtoEncontrado);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
        }
    }
}
