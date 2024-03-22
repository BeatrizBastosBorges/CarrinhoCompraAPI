using Microsoft.EntityFrameworkCore;
using MinhaAPI.Domain.Models;

namespace MinhaAPI.Infrastructure.Data.Contexts
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options)
        {
        }

        public DbSet<ProdutoModel> Produto { get; set; }
        public DbSet<CarrinhoModel> Carrinho { get; set; }
        public DbSet<CompraModel> Compra { get; set; }

        public DbSet<CompraProdutoModel> CompraProdutos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProdutoModel>().ToTable("Produto").HasKey(p => p.Id);
            modelBuilder.Entity<CarrinhoModel>().ToTable("Carrinho").HasKey(c => c.Id);
            modelBuilder.Entity<CompraModel>().ToTable("Compra").HasKey(o => o.Id);
            modelBuilder.Entity<CompraProdutoModel>().ToTable("CompraProduto").HasKey(cp => cp.Id);

            modelBuilder.Entity<CarrinhoModel>()
            .HasOne(c => c.Produto)
            .WithMany()
            .HasForeignKey(c => c.ProdutoId);

            modelBuilder.Entity<CompraProdutoModel>()
            .HasOne(cp => cp.Produto)
            .WithMany(p => p.ComprasProduto)
            .HasForeignKey(cp => cp.ProdutoId);

            base.OnModelCreating(modelBuilder);
        }
    }
}