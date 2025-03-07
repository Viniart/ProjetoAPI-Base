
using Microsoft.EntityFrameworkCore;
using ProjetoAPI.Context;
using ProjetoAPI.Model;
namespace ProjetoAPI.Endpoints
{
    public static class CategoriaEndpoints
    {
        public static void MapCategoriasEndpoints(this WebApplication app)
        {
            app.MapGet("/categorias", async (ProdutosDbContext db) =>
            {
                return await db.Categorias.ToListAsync();
            });

            app.MapPost("/categorias", async (Categoria categoria, ProdutosDbContext db) =>
            {
                if (categoria == null) return Results.BadRequest("Requisição Inválida!");

                db.Categorias.Add(categoria);
                await db.SaveChangesAsync();

                return Results.Created($"/categorias/{categoria.Id}", categoria);
            });
        }
    }
}
