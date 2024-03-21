using Microsoft.EntityFrameworkCore.Migrations;

namespace MinhaAPI.Migrations
{
    public partial class AtualizacaCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abate");

            migrationBuilder.AddColumn<int>(
                name: "QtdParcelasAtual",
                table: "Compra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "ValorRestante",
                table: "Compra",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QtdParcelasAtual",
                table: "Compra");

            migrationBuilder.DropColumn(
                name: "ValorRestante",
                table: "Compra");

            migrationBuilder.CreateTable(
                name: "Abate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompraId = table.Column<int>(type: "int", nullable: false),
                    NovoValorParcelaAuxiliar = table.Column<double>(type: "float", nullable: false),
                    NovoValorParcelas = table.Column<double>(type: "float", nullable: false),
                    NovoValorTotalCompra = table.Column<double>(type: "float", nullable: false),
                    ValorAbate = table.Column<double>(type: "float", nullable: false)
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
    }
}
