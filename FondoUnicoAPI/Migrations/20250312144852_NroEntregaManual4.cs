using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FondoUnicoAPI.Migrations
{
    /// <inheritdoc />
    public partial class NroEntregaManual4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NroEntregaManual",
                table: "Entregas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NroEntregaManual",
                table: "Entregas");
        }
    }
}
