using Microsoft.EntityFrameworkCore.Migrations;

namespace MinhaAPI.Migrations
{
    public partial class AbateModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValorParcela",
                table: "Compra",
                newName: "ValorParcelas");

            migrationBuilder.CreateTable(
                name: "Abate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValorAbate = table.Column<double>(type: "float", nullable: false),
                    NovoValorTotalCompra = table.Column<double>(type: "float", nullable: false),
                    NovoValorParcelas = table.Column<double>(type: "float", nullable: false),
                    NovoValorParcelaAuxiliar = table.Column<double>(type: "float", nullable: false),
                    CompraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abate_Compra_CompraId",
                        column: x => x.CompraId,
                        principalTable: "Compra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abate_CompraId",
                table: "Abate",
                column: "CompraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abate");

            migrationBuilder.RenameColumn(
                name: "ValorParcelas",
                table: "Compra",
                newName: "ValorParcela");
        }
    }
}
