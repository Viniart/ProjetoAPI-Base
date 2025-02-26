using Microsoft.EntityFrameworkCore;
using ProjetoAPI.Model;

namespace ProjetoAPI.Context
{
    public class ProdutosDbContext : DbContext
    {
        public ProdutosDbContext(DbContextOptions<ProdutosDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}
