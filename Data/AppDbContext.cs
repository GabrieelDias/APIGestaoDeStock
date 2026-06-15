using FluxoDeEstoque.Models;
using Microsoft.EntityFrameworkCore;

namespace FluxoDeEstoque.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

    }
}
