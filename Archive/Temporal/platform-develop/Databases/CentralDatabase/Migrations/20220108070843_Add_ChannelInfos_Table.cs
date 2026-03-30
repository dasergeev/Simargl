using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_ChannelInfos_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChannelInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Sampling = table.Column<double>(type: "float", nullable: false),
                    Cutoff = table.Column<double>(type: "float", nullable: false),
                    FrameId = table.Column<long>(type: "bigint", nullable: false),
                    NameId = table.Column<long>(type: "bigint", nullable: false),
                    UnitId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelInfos", x => x.Id);
                    table.UniqueConstraint("AK_ChannelInfos_Index_FrameId", x => new { x.Index, x.FrameId });
                    table.ForeignKey(
                        name: "FK_ChannelInfos_ChannelNames",
                        column: x => x.NameId,
                        principalTable: "ChannelNames",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChannelInfos_ChannelUnits",
                        column: x => x.UnitId,
                        principalTable: "ChannelUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChannelInfos_FrameInfos",
                        column: x => x.FrameId,
                        principalTable: "FrameInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChannelInfos_Cutoff",
                table: "ChannelInfos",
                column: "Cutoff");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelInfos_FrameId",
                table: "ChannelInfos",
                column: "FrameId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelInfos_Id",
                table: "ChannelInfos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelInfos_Index",
                table: "ChannelInfos",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelInfos_NameId",
                table: "ChannelInfos",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelInfos_Sampling",
                table: "ChannelInfos",
                column: "Sampling");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelInfos_UnitId",
                table: "ChannelInfos",
                column: "UnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChannelInfos");
        }
    }
}
