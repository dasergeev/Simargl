using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_Geolocations_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Geolocations",
                columns: table => new
                {
                    RegistrarId = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false),
                    IsAnalyzed = table.Column<bool>(type: "bit", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    Altitude = table.Column<double>(type: "float", nullable: true),
                    Speed = table.Column<double>(type: "float", nullable: true),
                    PoleCourse = table.Column<double>(type: "float", nullable: true),
                    MagneticCourse = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geolocations", x => new { x.RegistrarId, x.Timestamp });
                    table.ForeignKey(
                        name: "FK_Geolocations_Registrars",
                        column: x => x.RegistrarId,
                        principalTable: "Registrars",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Geolocations_IsAnalyzed",
                table: "Geolocations",
                column: "IsAnalyzed");

            migrationBuilder.CreateIndex(
                name: "IX_Geolocations_Latitude",
                table: "Geolocations",
                column: "Latitude");

            migrationBuilder.CreateIndex(
                name: "IX_Geolocations_Longitude",
                table: "Geolocations",
                column: "Longitude");

            migrationBuilder.CreateIndex(
                name: "IX_Geolocations_RegistrarId",
                table: "Geolocations",
                column: "RegistrarId");

            migrationBuilder.CreateIndex(
                name: "IX_Geolocations_Speed",
                table: "Geolocations",
                column: "Speed");

            migrationBuilder.CreateIndex(
                name: "IX_Geolocations_Time",
                table: "Geolocations",
                column: "Time");

            migrationBuilder.CreateIndex(
                name: "IX_Geolocations_Timestamp",
                table: "Geolocations",
                column: "Timestamp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Geolocations");
        }
    }
}
