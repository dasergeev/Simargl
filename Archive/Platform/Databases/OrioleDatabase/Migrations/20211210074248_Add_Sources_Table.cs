using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_Sources_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SensorId = table.Column<int>(type: "int", nullable: false),
                    ChannelId = table.Column<int>(type: "int", nullable: false),
                    BeginTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Format = table.Column<int>(type: "int", nullable: false),
                    Signal = table.Column<int>(type: "int", nullable: false),
                    Sampling = table.Column<double>(type: "float", nullable: false),
                    Cutoff = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sources_Channels",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sources_Sensors",
                        column: x => x.SensorId,
                        principalTable: "Sensors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sources_BeginTime",
                table: "Sources",
                column: "BeginTime");

            migrationBuilder.CreateIndex(
                name: "IX_Sources_ChannelId",
                table: "Sources",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Sources_EndTime",
                table: "Sources",
                column: "EndTime");

            migrationBuilder.CreateIndex(
                name: "IX_Sources_Format",
                table: "Sources",
                column: "Format");

            migrationBuilder.CreateIndex(
                name: "IX_Sources_Sampling",
                table: "Sources",
                column: "Sampling");

            migrationBuilder.CreateIndex(
                name: "IX_Sources_SensorId",
                table: "Sources",
                column: "SensorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sources");
        }
    }
}
