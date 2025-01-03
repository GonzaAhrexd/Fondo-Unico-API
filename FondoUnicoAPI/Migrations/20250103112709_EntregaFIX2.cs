using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FondoUnicoAPI.Migrations
{
    /// <inheritdoc />
    public partial class EntregaFIX2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RenglonesEntregas_Entregas_NroEntregaFK",
                table: "RenglonesEntregas");

            migrationBuilder.DropIndex(
                name: "IX_RenglonesEntregas_NroEntregaFK",
                table: "RenglonesEntregas");

            migrationBuilder.AddColumn<int>(
                name: "EntregasNroEntrega",
                table: "RenglonesEntregas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RenglonesEntregas_EntregasNroEntrega",
                table: "RenglonesEntregas",
                column: "EntregasNroEntrega");

            migrationBuilder.AddForeignKey(
                name: "FK_RenglonesEntregas_Entregas_EntregasNroEntrega",
                table: "RenglonesEntregas",
                column: "EntregasNroEntrega",
                principalTable: "Entregas",
                principalColumn: "NroEntrega");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RenglonesEntregas_Entregas_EntregasNroEntrega",
                table: "RenglonesEntregas");

            migrationBuilder.DropIndex(
                name: "IX_RenglonesEntregas_EntregasNroEntrega",
                table: "RenglonesEntregas");

            migrationBuilder.DropColumn(
                name: "EntregasNroEntrega",
                table: "RenglonesEntregas");

            migrationBuilder.CreateIndex(
                name: "IX_RenglonesEntregas_NroEntregaFK",
                table: "RenglonesEntregas",
                column: "NroEntregaFK");

            migrationBuilder.AddForeignKey(
                name: "FK_RenglonesEntregas_Entregas_NroEntregaFK",
                table: "RenglonesEntregas",
                column: "NroEntregaFK",
                principalTable: "Entregas",
                principalColumn: "NroEntrega",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
