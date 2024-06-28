using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acceso_Datos.Migrations
{
    public partial class Migracion_De_Inicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    Id_Compra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaRealizada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Correlativo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.Id_Compra);
                });

            migrationBuilder.CreateTable(
                name: "Zapatos",
                columns: table => new
                {
                    Id_Zapato = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zapatos", x => x.Id_Zapato);
                });

            migrationBuilder.CreateTable(
                name: "DetalleCompras",
                columns: table => new
                {
                    Id_DetalleCompra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Precio_Producto = table.Column<double>(type: "float", nullable: false),
                    IdCompraEnDetalle = table.Column<int>(type: "int", nullable: false),
                    IdZapatoEnDetalle = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleCompras", x => x.Id_DetalleCompra);
                    table.ForeignKey(
                        name: "FK_DetalleCompras_Compras_IdCompraEnDetalle",
                        column: x => x.IdCompraEnDetalle,
                        principalTable: "Compras",
                        principalColumn: "Id_Compra",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleCompras_Zapatos_IdZapatoEnDetalle",
                        column: x => x.IdZapatoEnDetalle,
                        principalTable: "Zapatos",
                        principalColumn: "Id_Zapato",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompras_IdCompraEnDetalle",
                table: "DetalleCompras",
                column: "IdCompraEnDetalle");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompras_IdZapatoEnDetalle",
                table: "DetalleCompras",
                column: "IdZapatoEnDetalle");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleCompras");

            migrationBuilder.DropTable(
                name: "Compras");

            migrationBuilder.DropTable(
                name: "Zapatos");
        }
    }
}
