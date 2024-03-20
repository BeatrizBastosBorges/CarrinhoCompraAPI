using Microsoft.EntityFrameworkCore.Migrations;

namespace MinhaAPI.Migrations
{
    public partial class ConctandoComprasECarrinho : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompraModelId",
                table: "Carrinho",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carrinho_CompraModelId",
                table: "Carrinho",
                column: "CompraModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carrinho_Compra_CompraModelId",
                table: "Carrinho",
                column: "CompraModelId",
                principalTable: "Compra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carrinho_Compra_CompraModelId",
                table: "Carrinho");

            migrationBuilder.DropIndex(
                name: "IX_Carrinho_CompraModelId",
                table: "Carrinho");

            migrationBuilder.DropColumn(
                name: "CompraModelId",
                table: "Carrinho");
        }
    }
}
