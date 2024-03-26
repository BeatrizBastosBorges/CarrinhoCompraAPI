﻿// <auto-generated />
using CarrinhoCompraAPI.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MinhaAPI.Migrations
{
    [DbContext(typeof(SqlServerContext))]
    [Migration("20240319183623_AbateModel")]
    partial class AbateModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("MinhaAPI.Domain.Models.AbateModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CompraId")
                        .HasColumnType("int");

                    b.Property<double>("NovoValorParcelaAuxiliar")
                        .HasColumnType("float");

                    b.Property<double>("NovoValorParcelas")
                        .HasColumnType("float");

                    b.Property<double>("NovoValorTotalCompra")
                        .HasColumnType("float");

                    b.Property<double>("ValorAbate")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CompraId");

                    b.ToTable("Abate");
                });

            modelBuilder.Entity("MinhaAPI.Domain.Models.CarrinhoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("QtdProduto")
                        .HasColumnType("int");

                    b.Property<double>("ValorTotalProduto")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ProdutoId");

                    b.ToTable("Carrinho");
                });

            modelBuilder.Entity("MinhaAPI.Domain.Models.CompraModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("QtdParcelas")
                        .HasColumnType("int");

                    b.Property<double>("ValorParcelaAuxiliar")
                        .HasColumnType("float");

                    b.Property<double>("ValorParcelas")
                        .HasColumnType("float");

                    b.Property<double>("ValorTotalCompra")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Compra");
                });

            modelBuilder.Entity("MinhaAPI.Domain.Models.ProdutoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("nomeProduto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("precoUnitarioProduto")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("MinhaAPI.Domain.Models.AbateModel", b =>
                {
                    b.HasOne("MinhaAPI.Domain.Models.CompraModel", "Compra")
                        .WithMany()
                        .HasForeignKey("CompraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compra");
                });

            modelBuilder.Entity("MinhaAPI.Domain.Models.CarrinhoModel", b =>
                {
                    b.HasOne("MinhaAPI.Domain.Models.ProdutoModel", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produto");
                });
#pragma warning restore 612, 618
        }
    }
}
