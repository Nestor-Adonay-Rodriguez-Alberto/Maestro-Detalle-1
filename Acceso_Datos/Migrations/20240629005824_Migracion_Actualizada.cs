using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acceso_Datos.Migrations
{
    public partial class Migracion_Actualizada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "Compras",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "Compras");
        }
    }
}
