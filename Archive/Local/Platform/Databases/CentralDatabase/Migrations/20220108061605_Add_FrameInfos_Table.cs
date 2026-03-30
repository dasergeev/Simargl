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
                    FileId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrameInfos", x => x.Id);
                    table.UniqueConstraint("AK_FrameInfos_FileId", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_FrameInfos_InternalFiles",
                        column: x => x.FileId,
                        principalTable: "InternalFiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FrameInfos_FileId",
                table: "FrameInfos",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_FrameInfos_Id",
                table: "FrameInfos",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FrameInfos");
        }
    }
}
