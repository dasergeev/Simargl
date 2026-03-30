using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_Statistics_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Statistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChannelId = table.Column<int>(type: "int", nullable: false),
                    RegistrarId = table.Column<int>(type: "int", nullable: false),
                    Cutoff = table.Column<double>(type: "float", nullable: false),
                    _FilterCutoff = table.Column<double>(type: "float", nullable: true),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.Id);
                    table.UniqueConstraint("AK_Statistics_ChannelId_RegistrarId_Timestamp_Cutoff", x => new { x.ChannelId, x.RegistrarId, x.Timestamp, x.Cutoff });
                    table.ForeignKey(
                        name: "FK_Statistics_Channels",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Statistics_Filters",
                        column: x => x._FilterCutoff,
                        principalTable: "Filters",
                        principalColumn: "Cutoff",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Statistics_Frames",
                        columns: x => new { x.RegistrarId, x.Timestamp },
                        principalTable: "Frames",
                        principalColumns: new[] { "RegistrarId", "Timestamp" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Statistics_Registrars",
                        column: x => x.RegistrarId,
                        principalTable: "Registrars",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Statistics__FilterCutoff",
                table: "Statistics",
                column: "_FilterCutoff");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_ChannelId",
                table: "Statistics",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_Cutoff",
                table: "Statistics",
                column: "Cutoff");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_Id",
                table: "Statistics",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_RegistrarId",
                table: "Statistics",
                column: "RegistrarId");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_RegistrarId_Timestamp",
                table: "Statistics",
                columns: new[] { "RegistrarId", "Timestamp" });

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_Timestamp",
                table: "Statistics",
                column: "Timestamp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Statistics");
        }
    }
}
