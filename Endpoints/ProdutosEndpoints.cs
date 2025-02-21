using Microsoft.EntityFrameworkCore;
using ProjetoAPI.Context;
using ProjetoAPI.Model;

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
            );

            //app.MapGet("/tarefas/{id}", async (int id, ProdutosDbContext db) =>
            //    await db.Produtos.FindAsync(id)
            //        is Produto produto
            //            ? Results.Ok(produto)
            //            : Results.NotFound())
            //    .WithName("GetProdutoById")
            //    .Produces<Produto>(StatusCodes.Status200OK)
            //    .Produces(StatusCodes.Status404NotFound);

            app.MapPost("/produtos", async (Produto prod, ProdutosDbContext db) =>
            {
                if (prod != null)
                {
                    db.Produtos.Add(prod);
                    await db.SaveChangesAsync();

                    return Results.Created($"/produtos/{prod.Id}", prod);
                }
                else
                {
                    return Results.BadRequest("Requisição Inválida!");
                }
            });

            app.MapPut("/produtos/{id}", async (Guid id, Produto produto, ProdutosDbContext db) =>
            {
                var produtoEncontrado = await db.Produtos.FindAsync(id);

                if (produtoEncontrado is null) return Results.NotFound();

                produtoEncontrado.Nome = produto.Nome;
                produtoEncontrado.Categoria = produto.Categoria;

                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            app.MapDelete("/produtos/{id}", async (Guid id, ProdutosDbContext db) =>
            {
                var produtoEncontrado = await db.Produtos.FindAsync(id);

                if (produtoEncontrado is null) return Results.NotFound();

                db.Produtos.Remove(produtoEncontrado);
                await db.SaveChangesAsync();
                return Results.Ok(produtoEncontrado);
            });
        }
    }
}
