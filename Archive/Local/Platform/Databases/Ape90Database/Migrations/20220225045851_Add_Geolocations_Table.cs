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
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAnalyzed = table.Column<bool>(type: "bit", nullable: false),
                    GpsTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    Altitude = table.Column<double>(type: "float", nullable: true),
                    Speed = table.Column<double>(type: "float", nullable: true),
                    PoleCourse = table.Column<double>(type: "float", nullable: true),
                    MagneticCourse = table.Column<double>(type: "float", nullable: true),
                    Solution = table.Column<int>(type: "int", nullable: true),
                    Satellites = table.Column<int>(type: "int", nullable: true),
                    Hdop = table.Column<double>(type: "float", nullable: true),
                    Geoidal = table.Column<double>(type: "float", nullable: true),
                    Age = table.Column<double>(type: "float", nullable: true),
                    Station = table.Column<int>(type: "int", nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: true),
                    Knots = table.Column<double>(type: "float", nullable: true),
                    MagneticVariation = table.Column<double>(type: "float", nullable: true),
                    Mode = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geolocations", x => x.Id);
                    table.UniqueConstraint("AK_Geolocations_RecTime", x => x.RecTime);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Geolocations");
        }
    }
}
