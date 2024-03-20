using Microsoft.EntityFrameworkCore.Migrations;

namespace MinhaAPI.Migrations
{
    public partial class AddCompraProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "precoUnitarioProduto",
                table: "Produto",
                newName: "PrecoUnitarioProduto");

            migrationBuilder.RenameColumn(
                name: "nomeProduto",
                table: "Produto",
                newName: "NomeProduto");

            migrationBuilder.CreateTable(
                name: "CompraProduto",
                columns: table => new
                {
                    CompraId = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraProduto", x => new { x.CompraId, x.ProdutoId });
                    table.ForeignKey(
                        name: "FK_CompraProduto_Compra_CompraId",
                        column: x => x.CompraId,
                        principalTable: "Compra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompraProduto_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompraProduto_ProdutoId",
                table: "CompraProduto",
                column: "ProdutoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompraProduto");

            migrationBuilder.RenameColumn(
                name: "PrecoUnitarioProduto",
                table: "Produto",
                newName: "precoUnitarioProduto");

            migrationBuilder.RenameColumn(
                name: "NomeProduto",
                table: "Produto",
                newName: "nomeProduto");

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
    }
}
