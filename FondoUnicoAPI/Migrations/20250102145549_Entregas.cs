using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FondoUnicoAPI.Migrations
{
    /// <inheritdoc />
    public partial class Entregas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropTable(
                name: "Numerador");

            migrationBuilder.DropColumn(
                name: "Limite",
                table: "Valores");

            migrationBuilder.CreateTable(
                name: "Entregas",
                columns: table => new
                {
                    NroEntrega = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unidad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entregas", x => x.NroEntrega);
                });

            migrationBuilder.CreateTable(
                name: "RenglonesEntregas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NroEntregaFK = table.Column<int>(type: "int", nullable: false),
                    NroRenglon = table.Column<int>(type: "int", nullable: false),
                    TipoFormulario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    desde = table.Column<int>(type: "int", nullable: false),
                    hasta = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RenglonesEntregas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RenglonesEntregas_Entregas_NroEntregaFK",
                        column: x => x.NroEntregaFK,
                        principalTable: "Entregas",
                        principalColumn: "NroEntrega",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RenglonesEntregas_NroEntregaFK",
                table: "RenglonesEntregas",
                column: "NroEntregaFK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RenglonesEntregas");

            migrationBuilder.DropTable(
                name: "Entregas");

            migrationBuilder.AddColumn<DateTime>(
                name: "Limite",
                table: "Valores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Numerador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Objeto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ultimoNumero = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Numerador", x => x.Id);
                });
        }
    }
}
