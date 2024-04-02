using Microsoft.EntityFrameworkCore.Migrations;

namespace MinhaAPI.Migrations
{
    public partial class AtributosDeProdutoEmCompraProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QtdParcelas",
                table: "CompraProduto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QtdParcelasAtual",
                table: "CompraProduto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "ValorAbatidoProduto",
                table: "CompraProduto",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValorParcelaAuxiliarProduto",
                table: "CompraProduto",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValorParcelasProduto",
                table: "CompraProduto",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValorRestanteProduto",
                table: "CompraProduto",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValorTotalProduto",
                table: "CompraProduto",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QtdParcelas",
                table: "CompraProduto");

            migrationBuilder.DropColumn(
                name: "QtdParcelasAtual",
                table: "CompraProduto");

            migrationBuilder.DropColumn(
                name: "ValorAbatidoProduto",
                table: "CompraProduto");

            migrationBuilder.DropColumn(
                name: "ValorParcelaAuxiliarProduto",
                table: "CompraProduto");

            migrationBuilder.DropColumn(
                name: "ValorParcelasProduto",
                table: "CompraProduto");

            migrationBuilder.DropColumn(
                name: "ValorRestanteProduto",
                table: "CompraProduto");

            migrationBuilder.DropColumn(
                name: "ValorTotalProduto",
                table: "CompraProduto");
        }
    }
}
