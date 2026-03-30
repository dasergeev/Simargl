using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_Geolocation_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GgaMessages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<TimeSpan>(type: "time", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    Solution = table.Column<int>(type: "int", nullable: true),
                    Satellites = table.Column<int>(type: "int", nullable: true),
                    Hdop = table.Column<double>(type: "float", nullable: true),
                    Altitude = table.Column<double>(type: "float", nullable: true),
                    Geoidal = table.Column<double>(type: "float", nullable: true),
                    Age = table.Column<double>(type: "float", nullable: true),
                    Station = table.Column<int>(type: "int", nullable: true),
                    RawGeolocationId = table.Column<long>(type: "bigint", nullable: false),
                    FileTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    IsAnalyzed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GgaMessages", x => x.Id);
                    table.UniqueConstraint("AK_GgaMessages_RawGeolocationId_Index", x => new { x.RawGeolocationId, x.Index });
                    table.ForeignKey(
                        name: "FK_GgaMessages_RawGeolocations",
                        column: x => x.RawGeolocationId,
                        principalTable: "RawGeolocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RmcMessages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<TimeSpan>(type: "time", nullable: true),
                    Valid = table.Column<bool>(type: "bit", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    Knots = table.Column<double>(type: "float", nullable: true),
                    Speed = table.Column<double>(type: "float", nullable: true),
                    PoleCourse = table.Column<double>(type: "float", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MagneticVariation = table.Column<double>(type: "float", nullable: true),
                    MagneticCourse = table.Column<double>(type: "float", nullable: true),
                    Mode = table.Column<int>(type: "int", nullable: true),
                    RawGeolocationId = table.Column<long>(type: "bigint", nullable: false),
                    FileTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    IsAnalyzed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RmcMessages", x => x.Id);
                    table.UniqueConstraint("AK_RmcMessages_RawGeolocationId_Index", x => new { x.RawGeolocationId, x.Index });
                    table.ForeignKey(
                        name: "FK_RmcMessages_RawGeolocations",
                        column: x => x.RawGeolocationId,
                        principalTable: "RawGeolocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VtgMessages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PoleCourse = table.Column<double>(type: "float", nullable: true),
                    MagneticCourse = table.Column<double>(type: "float", nullable: true),
                    Knots = table.Column<double>(type: "float", nullable: true),
                    Speed = table.Column<double>(type: "float", nullable: true),
                    Mode = table.Column<int>(type: "int", nullable: true),
                    RawGeolocationId = table.Column<long>(type: "bigint", nullable: false),
                    FileTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    IsAnalyzed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VtgMessages", x => x.Id);
                    table.UniqueConstraint("AK_VtgMessages_RawGeolocationId_Index", x => new { x.RawGeolocationId, x.Index });
                    table.ForeignKey(
                        name: "FK_VtgMessages_RawGeolocations",
                        column: x => x.RawGeolocationId,
                        principalTable: "RawGeolocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GgaMessages");

            migrationBuilder.DropTable(
                name: "RmcMessages");

            migrationBuilder.DropTable(
                name: "VtgMessages");
        }
    }
}
