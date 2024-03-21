using Microsoft.EntityFrameworkCore.Migrations;

namespace MinhaAPI.Migrations
{
    public partial class adaptacaoEmCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "CompraProduto");

            migrationBuilder.AddColumn<double>(
                name: "ValorAbatido",
                table: "Compra",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorAbatido",
                table: "Compra");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CompraProduto",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
