using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_Locations_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Altitude = table.Column<double>(type: "float", nullable: false),
                    FirstVerticalRadius = table.Column<double>(type: "float", nullable: false),
                    X = table.Column<double>(type: "float", nullable: false),
                    Y = table.Column<double>(type: "float", nullable: false),
                    Z = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.UniqueConstraint("AK_Locations_Latitude_Longitude", x => new { x.Latitude, x.Longitude });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_Altitude",
                table: "Locations",
                column: "Altitude");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_FirstVerticalRadius",
                table: "Locations",
                column: "FirstVerticalRadius");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_Id",
                table: "Locations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_Latitude",
                table: "Locations",
                column: "Latitude");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_Longitude",
                table: "Locations",
                column: "Longitude");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_X",
                table: "Locations",
                column: "X");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_Y",
                table: "Locations",
                column: "Y");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_Z",
                table: "Locations",
                column: "Z");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
