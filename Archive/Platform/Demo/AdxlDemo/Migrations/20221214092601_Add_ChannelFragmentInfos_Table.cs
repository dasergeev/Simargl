using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Demo.Migrations
{
    internal partial class Add_ChannelFragmentInfos_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChannelFragmentInfos",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChannelInfoID = table.Column<long>(type: "INTEGER", nullable: false),
                    Path = table.Column<string>(type: "TEXT", nullable: false),
                    Sampling = table.Column<double>(type: "REAL", nullable: false),
                    Cutoff = table.Column<double>(type: "REAL", nullable: false),
                    BeginTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MinValue = table.Column<double>(type: "REAL", nullable: false),
                    MaxValue = table.Column<double>(type: "REAL", nullable: false),
                    Average = table.Column<double>(type: "REAL", nullable: false),
                    Deviation = table.Column<double>(type: "REAL", nullable: false),
                    Count = table.Column<int>(type: "INTEGER", nullable: false),
                    Sum = table.Column<double>(type: "REAL", nullable: false),
                    SumSquares = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelFragmentInfos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ChannelFragmentInfos_ChannelInfos",
                        column: x => x.ChannelInfoID,
                        principalTable: "ChannelInfos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChannelFragmentInfos_BeginTime",
                table: "ChannelFragmentInfos",
                column: "BeginTime",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChannelFragmentInfos_ChannelInfoID",
                table: "ChannelFragmentInfos",
                column: "ChannelInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelFragmentInfos_EndTime",
                table: "ChannelFragmentInfos",
                column: "EndTime",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChannelFragmentInfos");
        }
    }
}
