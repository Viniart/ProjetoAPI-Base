using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProjetoAPI.Context;
using ProjetoAPI.Model;
using System.ComponentModel.DataAnnotations;

namespace ProjetoAPI.Endpoints
{
    public static class ProdutosEndpoints
    {
        public static void MapProdutosEndpoints(this WebApplication app)
        {
            app.MapGet("/produtos", async (ProdutosDbContext db) =>
            await db.Produtos.ToListAsync());

            app.MapGet("/produtos/{id}", async (Guid id, ProdutosDbContext db) =>
                await db.Produtos.FindAsync(id)
                    is Produto produto
                        ? Results.Ok(produto)
                        : Results.NotFound()
            )
            .Produces<Produto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            app.MapPost("/produtos", async (Produto prod, IValidator<Produto> validator, ProdutosDbContext db) =>
            {
                if (prod != null)
                {
                    // Validando no Cadastro 
                    var validation = await validator.ValidateAsync(prod);

                    if (!validation.IsValid) return Results.ValidationProblem(validation.ToDictionary());

                    db.Produtos.Add(prod);
                    await db.SaveChangesAsync();

                    return Results.Created($"/produtos/{prod.Id}", prod);
                }
                else
                {
                    return Results.BadRequest("Requisição Inválida!");
                }
            })
            .Produces(StatusCodes.Status400BadRequest)
            .Produces<Produto>(StatusCodes.Status201Created);

            app.MapPut("/produtos/{id}", async (Guid id, Produto produto, IValidator<Produto> validator, ProdutosDbContext db) =>
            {
                var validation = await validator.ValidateAsync(produto);

                if (!validation.IsValid) return Results.ValidationProblem(validation.ToDictionary());

                var produtoEncontrado = await db.Produtos.FindAsync(id);

                if (produtoEncontrado is null) return Results.NotFound();

                produtoEncontrado.Nome = produto.Nome;
                produtoEncontrado.Categoria = produto.Categoria;

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
