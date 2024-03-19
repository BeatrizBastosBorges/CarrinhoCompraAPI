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
        public DbSet<AbateModel> Abate { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProdutoModel>().ToTable("Produto").HasKey(p => p.Id);
            modelBuilder.Entity<CarrinhoModel>().ToTable("Carrinho").HasKey(c => c.Id);
            modelBuilder.Entity<CompraModel>().ToTable("Compra").HasKey(o => o.Id);
            modelBuilder.Entity<AbateModel>().ToTable("Abate").HasKey(a => a.Id);

            modelBuilder.Entity<CarrinhoModel>()
            .HasOne(c => c.Produto)
            .WithMany()
            .HasForeignKey(c => c.ProdutoId);

            modelBuilder.Entity<AbateModel>()
            .HasOne(o => o.Compra)
            .WithMany()
            .HasForeignKey(o => o.CompraId);

            base.OnModelCreating(modelBuilder);
        }
    }
}