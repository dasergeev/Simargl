using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_MapNodes_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MapNodes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapNodes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MapNodes_Id",
                table: "MapNodes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MapNodes_Latitude",
                table: "MapNodes",
                column: "Latitude");

            migrationBuilder.CreateIndex(
                name: "IX_MapNodes_Longitude",
                table: "MapNodes",
                column: "Longitude");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MapNodes");
        }
    }
}
