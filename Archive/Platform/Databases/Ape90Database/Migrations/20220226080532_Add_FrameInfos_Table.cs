using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_FrameInfos_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FrameInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsAnalyzed = table.Column<bool>(type: "bit", nullable: false),
                    Duration = table.Column<double>(type: "float", nullable: false),
                    BeginTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrameInfos", x => x.Id);
                    table.UniqueConstraint("AK_FrameInfos_BeginTime_EndTime", x => new { x.BeginTime, x.EndTime });
                    table.UniqueConstraint("AK_FrameInfos_Path", x => x.Path);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FrameInfos_BeginTime",
                table: "FrameInfos",
                column: "BeginTime");

            migrationBuilder.CreateIndex(
                name: "IX_FrameInfos_EndTime",
                table: "FrameInfos",
                column: "EndTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FrameInfos");
        }
    }
}
