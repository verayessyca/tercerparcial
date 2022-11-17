using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimerParcial.Migrations
{
    public partial class segundamigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Ciudad",
                table: "Ciudad");

            migrationBuilder.RenameTable(
                name: "Ciudad",
                newName: "Ciudades");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ciudades",
                table: "Ciudades",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Ciudades",
                table: "Ciudades");

            migrationBuilder.RenameTable(
                name: "Ciudades",
                newName: "Ciudad");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ciudad",
                table: "Ciudad",
                column: "Id");
        }
    }
}
