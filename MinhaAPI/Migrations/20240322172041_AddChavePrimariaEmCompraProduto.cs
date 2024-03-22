using Microsoft.EntityFrameworkCore.Migrations;

namespace MinhaAPI.Migrations
{
    public partial class AddChavePrimariaEmCompraProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CompraProduto",
                table: "CompraProduto");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CompraProduto",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompraProduto",
                table: "CompraProduto",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CompraProduto_CompraId",
                table: "CompraProduto",
                column: "CompraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CompraProduto",
                table: "CompraProduto");

            migrationBuilder.DropIndex(
                name: "IX_CompraProduto_CompraId",
                table: "CompraProduto");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CompraProduto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompraProduto",
                table: "CompraProduto",
                columns: new[] { "CompraId", "ProdutoId" });
        }
    }
}
