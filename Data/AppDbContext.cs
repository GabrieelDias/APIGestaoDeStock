using FluxoDeEstoque.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FluxoDeEstoque.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Imagem> ProdutoImagens { get; set; }
        public DbSet<Cupom> Cupons { get; set; }
        public DbSet<Carrinho> Carrinhos { get; set; }
        public DbSet<CarrinhoItem> CarrinhoItens { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidoItens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Produto>()
                .HasIndex(p => p.SKU)
                .IsUnique();

          
            modelBuilder.Entity<Cupom>()
                .HasIndex(c => c.Codigo)
                .IsUnique();

           
            modelBuilder.Entity<Produto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict); 

            
            modelBuilder.Entity<PedidoItem>()
                .HasOne(pi => pi.Produto)
                .WithMany()
                .HasForeignKey(pi => pi.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
