using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class End_Reorganization_Nmea_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLoaded",
                table: "NmeaFiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "GgaMessages",
                columns: table => new
                {
                    RawDirectoryId = table.Column<int>(type: "int", nullable: false),
                    FileTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
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
                    RegistrarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GgaMessages", x => new { x.RawDirectoryId, x.FileTime, x.Index });
                    table.ForeignKey(
                        name: "FK_GgaMessages_NmeaFiles",
                        columns: x => new { x.RawDirectoryId, x.FileTime },
                        principalTable: "NmeaFiles",
                        principalColumns: new[] { "RawDirectoryId", "Time" });
                    table.ForeignKey(
                        name: "FK_GgaMessages_RawDirectories",
                        column: x => x.RawDirectoryId,
                        principalTable: "RawDirectories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GgaMessages_Registrars",
                        column: x => x.RegistrarId,
                        principalTable: "Registrars",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RmcMessages",
                columns: table => new
                {
                    RawDirectoryId = table.Column<int>(type: "int", nullable: false),
                    FileTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
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
                    RegistrarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RmcMessages", x => new { x.RawDirectoryId, x.FileTime, x.Index });
                    table.ForeignKey(
                        name: "FK_RmcMessages_NmeaFiles",
                        columns: x => new { x.RawDirectoryId, x.FileTime },
                        principalTable: "NmeaFiles",
                        principalColumns: new[] { "RawDirectoryId", "Time" });
                    table.ForeignKey(
                        name: "FK_RmcMessages_RawDirectories",
                        column: x => x.RawDirectoryId,
                        principalTable: "RawDirectories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RmcMessages_Registrars",
                        column: x => x.RegistrarId,
                        principalTable: "Registrars",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VtgMessages",
                columns: table => new
                {
                    RawDirectoryId = table.Column<int>(type: "int", nullable: false),
                    FileTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    PoleCourse = table.Column<double>(type: "float", nullable: true),
                    MagneticCourse = table.Column<double>(type: "float", nullable: true),
                    Knots = table.Column<double>(type: "float", nullable: true),
                    Speed = table.Column<double>(type: "float", nullable: true),
                    Mode = table.Column<int>(type: "int", nullable: true),
                    RegistrarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VtgMessages", x => new { x.RawDirectoryId, x.FileTime, x.Index });
                    table.ForeignKey(
                        name: "FK_VtgMessages_NmeaFiles",
                        columns: x => new { x.RawDirectoryId, x.FileTime },
                        principalTable: "NmeaFiles",
                        principalColumns: new[] { "RawDirectoryId", "Time" });
                    table.ForeignKey(
                        name: "FK_VtgMessages_RawDirectories",
                        column: x => x.RawDirectoryId,
                        principalTable: "RawDirectories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VtgMessages_Registrars",
                        column: x => x.RegistrarId,
                        principalTable: "Registrars",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NmeaFiles_IsLoaded",
                table: "NmeaFiles",
                column: "IsLoaded");

            migrationBuilder.CreateIndex(
                name: "IX_GgaMessages_FileTime",
                table: "GgaMessages",
                column: "FileTime");

            migrationBuilder.CreateIndex(
                name: "IX_GgaMessages_Index",
                table: "GgaMessages",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_GgaMessages_RawDirectoryId",
                table: "GgaMessages",
                column: "RawDirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GgaMessages_RegistrarId",
                table: "GgaMessages",
                column: "RegistrarId");

            migrationBuilder.CreateIndex(
                name: "IX_RmcMessages_FileTime",
                table: "RmcMessages",
                column: "FileTime");

            migrationBuilder.CreateIndex(
                name: "IX_RmcMessages_Index",
                table: "RmcMessages",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_RmcMessages_RawDirectoryId",
                table: "RmcMessages",
                column: "RawDirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RmcMessages_RegistrarId",
                table: "RmcMessages",
                column: "RegistrarId");

            migrationBuilder.CreateIndex(
                name: "IX_VtgMessages_FileTime",
                table: "VtgMessages",
                column: "FileTime");

            migrationBuilder.CreateIndex(
                name: "IX_VtgMessages_Index",
                table: "VtgMessages",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_VtgMessages_RawDirectoryId",
                table: "VtgMessages",
                column: "RawDirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_VtgMessages_RegistrarId",
                table: "VtgMessages",
                column: "RegistrarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GgaMessages");

            migrationBuilder.DropTable(
                name: "RmcMessages");

            migrationBuilder.DropTable(
                name: "VtgMessages");

            migrationBuilder.DropIndex(
                name: "IX_NmeaFiles_IsLoaded",
                table: "NmeaFiles");

            migrationBuilder.DropColumn(
                name: "IsLoaded",
                table: "NmeaFiles");
        }
    }
}
