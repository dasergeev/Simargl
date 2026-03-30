using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_ReceivingTime_Field_In_NmeaMessages_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GgaMessages",
                columns: table => new
                {
                    RegistrarId = table.Column<int>(type: "int", nullable: false),
                    ReceivingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    RawDirectoryId = table.Column<int>(type: "int", nullable: false),
                    FileTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GgaMessages", x => new { x.RegistrarId, x.ReceivingTime });
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
                    RegistrarId = table.Column<int>(type: "int", nullable: false),
                    ReceivingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    RawDirectoryId = table.Column<int>(type: "int", nullable: false),
                    FileTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RmcMessages", x => new { x.RegistrarId, x.ReceivingTime });
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
                    RegistrarId = table.Column<int>(type: "int", nullable: false),
                    ReceivingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PoleCourse = table.Column<double>(type: "float", nullable: true),
                    MagneticCourse = table.Column<double>(type: "float", nullable: true),
                    Knots = table.Column<double>(type: "float", nullable: true),
                    Speed = table.Column<double>(type: "float", nullable: true),
                    Mode = table.Column<int>(type: "int", nullable: true),
                    RawDirectoryId = table.Column<int>(type: "int", nullable: false),
                    FileTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VtgMessages", x => new { x.RegistrarId, x.ReceivingTime });
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
                name: "IX_GgaMessages_RawDirectoryId_FileTime",
                table: "GgaMessages",
                columns: new[] { "RawDirectoryId", "FileTime" });

            migrationBuilder.CreateIndex(
                name: "IX_GgaMessages_ReceivingTime",
                table: "GgaMessages",
                column: "ReceivingTime");

            migrationBuilder.CreateIndex(
                name: "IX_GgaMessages_RegistrarId",
                table: "GgaMessages",
                column: "RegistrarId");

            migrationBuilder.CreateIndex(
                name: "IX_RmcMessages_RawDirectoryId_FileTime",
                table: "RmcMessages",
                columns: new[] { "RawDirectoryId", "FileTime" });

            migrationBuilder.CreateIndex(
                name: "IX_RmcMessages_ReceivingTime",
                table: "RmcMessages",
                column: "ReceivingTime");

            migrationBuilder.CreateIndex(
                name: "IX_RmcMessages_RegistrarId",
                table: "RmcMessages",
                column: "RegistrarId");

            migrationBuilder.CreateIndex(
                name: "IX_VtgMessages_RawDirectoryId_FileTime",
                table: "VtgMessages",
                columns: new[] { "RawDirectoryId", "FileTime" });

            migrationBuilder.CreateIndex(
                name: "IX_VtgMessages_ReceivingTime",
                table: "VtgMessages",
                column: "ReceivingTime");

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
        }
    }
}
