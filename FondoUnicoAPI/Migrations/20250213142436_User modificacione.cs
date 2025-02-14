using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FondoUnicoAPI.Migrations
{
    /// <inheritdoc />
    public partial class Usermodificacione : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DNI",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Unidad",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DNI",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Unidad",
                table: "Usuario");
        }
    }
}
