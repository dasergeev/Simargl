using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_Extremums_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsExtremum",
                table: "Frames",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Extremums",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrarId = table.Column<int>(type: "int", nullable: false),
                    FrameTimestamp = table.Column<long>(type: "bigint", nullable: false),
                    ChannelId = table.Column<int>(type: "int", nullable: false),
                    Cutoff = table.Column<double>(type: "float", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extremums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Extremums_Channels",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Extremums_Filters",
                        column: x => x.Cutoff,
                        principalTable: "Filters",
                        principalColumn: "Cutoff",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Extremums_Frames",
                        columns: x => new { x.RegistrarId, x.FrameTimestamp },
                        principalTable: "Frames",
                        principalColumns: new[] { "RegistrarId", "Timestamp" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Extremums_Locations",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Extremums_Registrars",
                        column: x => x.RegistrarId,
                        principalTable: "Registrars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Frames_IsExtremum",
                table: "Frames",
                column: "IsExtremum");

            migrationBuilder.CreateIndex(
                name: "IX_Extremums_ChannelId",
                table: "Extremums",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Extremums_Cutoff",
                table: "Extremums",
                column: "Cutoff");

            migrationBuilder.CreateIndex(
                name: "IX_Extremums_FrameTimestamp",
                table: "Extremums",
                column: "FrameTimestamp");

            migrationBuilder.CreateIndex(
                name: "IX_Extremums_Id",
                table: "Extremums",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Extremums_LocationId",
                table: "Extremums",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Extremums_RegistrarId",
                table: "Extremums",
                column: "RegistrarId");

            migrationBuilder.CreateIndex(
                name: "IX_Extremums_RegistrarId_FrameTimestamp",
                table: "Extremums",
                columns: new[] { "RegistrarId", "FrameTimestamp" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Extremums");

            migrationBuilder.DropIndex(
                name: "IX_Frames_IsExtremum",
                table: "Frames");

            migrationBuilder.DropColumn(
                name: "IsExtremum",
                table: "Frames");
        }
    }
}
