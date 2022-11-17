using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimerParcial.Migrations
{
    public partial class terceramigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCiudad",
                table: "Clientes");

            migrationBuilder.AddColumn<int>(
                name: "IdCiudadId",
                table: "Clientes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_IdCiudadId",
                table: "Clientes",
                column: "IdCiudadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Ciudades_IdCiudadId",
                table: "Clientes",
                column: "IdCiudadId",
                principalTable: "Ciudades",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Ciudades_IdCiudadId",
                table: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_IdCiudadId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "IdCiudadId",
                table: "Clientes");

            migrationBuilder.AddColumn<int>(
                name: "IdCiudad",
                table: "Clientes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
