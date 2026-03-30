using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations.Ape90DatabasePostgres
{
    internal partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChannelInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: false),
                    Sampling = table.Column<double>(type: "double precision", nullable: false),
                    Cutoff = table.Column<double>(type: "double precision", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelInfos", x => x.Id);
                    table.UniqueConstraint("AK_ChannelInfos_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "FrameInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Path = table.Column<string>(type: "text", nullable: false),
                    IsAnalyzed = table.Column<bool>(type: "boolean", nullable: false),
                    Duration = table.Column<double>(type: "double precision", nullable: false),
                    BeginTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrameInfos", x => x.Id);
                    table.UniqueConstraint("AK_FrameInfos_BeginTime_EndTime", x => new { x.BeginTime, x.EndTime });
                    table.UniqueConstraint("AK_FrameInfos_Path", x => x.Path);
                });

            migrationBuilder.CreateTable(
                name: "Geolocations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RecTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsAnalyzed = table.Column<bool>(type: "boolean", nullable: false),
                    GpsTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Latitude = table.Column<double>(type: "double precision", nullable: true),
                    Longitude = table.Column<double>(type: "double precision", nullable: true),
                    Altitude = table.Column<double>(type: "double precision", nullable: true),
                    Speed = table.Column<double>(type: "double precision", nullable: true),
                    PoleCourse = table.Column<double>(type: "double precision", nullable: true),
                    MagneticCourse = table.Column<double>(type: "double precision", nullable: true),
                    Solution = table.Column<int>(type: "integer", nullable: true),
                    Satellites = table.Column<int>(type: "integer", nullable: true),
                    Hdop = table.Column<double>(type: "double precision", nullable: true),
                    Geoidal = table.Column<double>(type: "double precision", nullable: true),
                    Age = table.Column<double>(type: "double precision", nullable: true),
                    Station = table.Column<int>(type: "integer", nullable: true),
                    Valid = table.Column<bool>(type: "boolean", nullable: true),
                    Knots = table.Column<double>(type: "double precision", nullable: true),
                    MagneticVariation = table.Column<double>(type: "double precision", nullable: true),
                    Mode = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geolocations", x => x.Id);
                    table.UniqueConstraint("AK_Geolocations_RecTime", x => x.RecTime);
                });

            migrationBuilder.CreateTable(
                name: "RawDirectories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Path = table.Column<string>(type: "text", nullable: false),
                    FrameDuration = table.Column<double>(type: "double precision", nullable: false),
                    BeginTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawDirectories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fragments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChannelInfoId = table.Column<long>(type: "bigint", nullable: false),
                    FrameInfoId = table.Column<long>(type: "bigint", nullable: false),
                    Index = table.Column<int>(type: "integer", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsLoadedIntoLongFrame = table.Column<bool>(type: "boolean", nullable: false),
                    IsDataValid = table.Column<bool>(type: "boolean", nullable: false),
                    SpeedZero = table.Column<double>(type: "double precision", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    Min = table.Column<double>(type: "double precision", nullable: false),
                    Max = table.Column<double>(type: "double precision", nullable: false),
                    Average = table.Column<double>(type: "double precision", nullable: false),
                    Deviation = table.Column<double>(type: "double precision", nullable: false),
                    Sum = table.Column<double>(type: "double precision", nullable: false),
                    SquaresSum = table.Column<double>(type: "double precision", nullable: false),
                    IsGpsValid = table.Column<bool>(type: "boolean", nullable: false),
                    Speed = table.Column<double>(type: "double precision", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    Altitude = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fragments", x => x.Id);
                    table.UniqueConstraint("AK_Fragments_ChannelInfoId_FrameInfoId_Index", x => new { x.ChannelInfoId, x.FrameInfoId, x.Index });
                    table.ForeignKey(
                        name: "FK_Fragments_ChannelInfos",
                        column: x => x.ChannelInfoId,
                        principalTable: "ChannelInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fragments_FrameInfos",
                        column: x => x.FrameInfoId,
                        principalTable: "FrameInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RawFrames",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsAnalyzed = table.Column<bool>(type: "boolean", nullable: false),
                    IsNormalizedTime = table.Column<bool>(type: "boolean", nullable: false),
                    Duration = table.Column<double>(type: "double precision", nullable: false),
                    BeginTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    RawDirectoryId = table.Column<long>(type: "bigint", nullable: false),
                    IsMetrics = table.Column<bool>(type: "boolean", nullable: false),
                    NameTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastAccessTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastWriteTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawFrames", x => x.Id);
                    table.UniqueConstraint("AK_RawFrames_Path", x => x.Path);
                    table.ForeignKey(
                        name: "FK_RawFrames_RawDirectories",
                        column: x => x.RawDirectoryId,
                        principalTable: "RawDirectories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RawGeolocations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsLoaded = table.Column<bool>(type: "boolean", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    RawDirectoryId = table.Column<long>(type: "bigint", nullable: false),
                    IsMetrics = table.Column<bool>(type: "boolean", nullable: false),
                    NameTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastAccessTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastWriteTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawGeolocations", x => x.Id);
                    table.UniqueConstraint("AK_RawGeolocations_Path", x => x.Path);
                    table.ForeignKey(
                        name: "FK_RawGeolocations_RawDirectories",
                        column: x => x.RawDirectoryId,
                        principalTable: "RawDirectories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RawPowers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Path = table.Column<string>(type: "text", nullable: false),
                    RawDirectoryId = table.Column<long>(type: "bigint", nullable: false),
                    IsMetrics = table.Column<bool>(type: "boolean", nullable: false),
                    NameTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastAccessTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastWriteTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawPowers", x => x.Id);
                    table.UniqueConstraint("AK_RawPowers_Path", x => x.Path);
                    table.ForeignKey(
                        name: "FK_RawPowers_RawDirectories",
                        column: x => x.RawDirectoryId,
                        principalTable: "RawDirectories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GgaMessages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Time = table.Column<TimeSpan>(type: "interval", nullable: true),
                    Latitude = table.Column<double>(type: "double precision", nullable: true),
                    Longitude = table.Column<double>(type: "double precision", nullable: true),
                    Solution = table.Column<int>(type: "integer", nullable: true),
                    Satellites = table.Column<int>(type: "integer", nullable: true),
                    Hdop = table.Column<double>(type: "double precision", nullable: true),
                    Altitude = table.Column<double>(type: "double precision", nullable: true),
                    Geoidal = table.Column<double>(type: "double precision", nullable: true),
                    Age = table.Column<double>(type: "double precision", nullable: true),
                    Station = table.Column<int>(type: "integer", nullable: true),
                    RawGeolocationId = table.Column<long>(type: "bigint", nullable: false),
                    FileTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Index = table.Column<int>(type: "integer", nullable: false),
                    IsAnalyzed = table.Column<bool>(type: "boolean", nullable: false)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Time = table.Column<TimeSpan>(type: "interval", nullable: true),
                    Valid = table.Column<bool>(type: "boolean", nullable: true),
                    Latitude = table.Column<double>(type: "double precision", nullable: true),
                    Longitude = table.Column<double>(type: "double precision", nullable: true),
                    Knots = table.Column<double>(type: "double precision", nullable: true),
                    Speed = table.Column<double>(type: "double precision", nullable: true),
                    PoleCourse = table.Column<double>(type: "double precision", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MagneticVariation = table.Column<double>(type: "double precision", nullable: true),
                    MagneticCourse = table.Column<double>(type: "double precision", nullable: true),
                    Mode = table.Column<int>(type: "integer", nullable: true),
                    RawGeolocationId = table.Column<long>(type: "bigint", nullable: false),
                    FileTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Index = table.Column<int>(type: "integer", nullable: false),
                    IsAnalyzed = table.Column<bool>(type: "boolean", nullable: false)
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PoleCourse = table.Column<double>(type: "double precision", nullable: true),
                    MagneticCourse = table.Column<double>(type: "double precision", nullable: true),
                    Knots = table.Column<double>(type: "double precision", nullable: true),
                    Speed = table.Column<double>(type: "double precision", nullable: true),
                    Mode = table.Column<int>(type: "integer", nullable: true),
                    RawGeolocationId = table.Column<long>(type: "bigint", nullable: false),
                    FileTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Index = table.Column<int>(type: "integer", nullable: false),
                    IsAnalyzed = table.Column<bool>(type: "boolean", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_Altitude",
                table: "Fragments",
                column: "Altitude");

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_Average",
                table: "Fragments",
                column: "Average");

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_Deviation",
                table: "Fragments",
                column: "Deviation");

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_FrameInfoId",
                table: "Fragments",
                column: "FrameInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_IsDataValid",
                table: "Fragments",
                column: "IsDataValid");

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_IsGpsValid",
                table: "Fragments",
                column: "IsGpsValid");

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_Latitude",
                table: "Fragments",
                column: "Latitude");

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_Longitude",
                table: "Fragments",
                column: "Longitude");

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_Max",
                table: "Fragments",
                column: "Max");

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_Min",
                table: "Fragments",
                column: "Min");

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_Speed",
                table: "Fragments",
                column: "Speed");

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_SpeedZero",
                table: "Fragments",
                column: "SpeedZero");

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_Sum",
                table: "Fragments",
                column: "Sum");

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_Time",
                table: "Fragments",
                column: "Time");

            migrationBuilder.CreateIndex(
                name: "IX_FrameInfos_BeginTime",
                table: "FrameInfos",
                column: "BeginTime");

            migrationBuilder.CreateIndex(
                name: "IX_FrameInfos_EndTime",
                table: "FrameInfos",
                column: "EndTime");

            migrationBuilder.CreateIndex(
                name: "IX_RawFrames_BeginTime",
                table: "RawFrames",
                column: "BeginTime");

            migrationBuilder.CreateIndex(
                name: "IX_RawFrames_EndTime",
                table: "RawFrames",
                column: "EndTime");

            migrationBuilder.CreateIndex(
                name: "IX_RawFrames_RawDirectoryId",
                table: "RawFrames",
                column: "RawDirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RawGeolocations_RawDirectoryId",
                table: "RawGeolocations",
                column: "RawDirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RawPowers_RawDirectoryId",
                table: "RawPowers",
                column: "RawDirectoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fragments");

            migrationBuilder.DropTable(
                name: "Geolocations");

            migrationBuilder.DropTable(
                name: "GgaMessages");

            migrationBuilder.DropTable(
                name: "RawFrames");

            migrationBuilder.DropTable(
                name: "RawPowers");

            migrationBuilder.DropTable(
                name: "RmcMessages");

            migrationBuilder.DropTable(
                name: "VtgMessages");

            migrationBuilder.DropTable(
                name: "ChannelInfos");

            migrationBuilder.DropTable(
                name: "FrameInfos");

            migrationBuilder.DropTable(
                name: "RawGeolocations");

            migrationBuilder.DropTable(
                name: "RawDirectories");
        }
    }
}
