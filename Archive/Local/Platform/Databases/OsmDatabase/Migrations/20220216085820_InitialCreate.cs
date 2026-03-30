using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OsmFiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsAnalyzed = table.Column<bool>(type: "bit", nullable: false),
                    IsEmpty = table.Column<bool>(type: "bit", nullable: false),
                    IsNodesLoad = table.Column<bool>(type: "bit", nullable: false),
                    IsTagsLoad = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OsmFiles", x => x.Id);
                    table.UniqueConstraint("AK_OsmFiles_FilePath", x => x.FilePath);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OsmFiles_FilePath",
                table: "OsmFiles",
                column: "FilePath");

            migrationBuilder.CreateIndex(
                name: "IX_OsmFiles_Id",
                table: "OsmFiles",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OsmFiles_IsAnalyzed",
                table: "OsmFiles",
                column: "IsAnalyzed");

            migrationBuilder.CreateIndex(
                name: "IX_OsmFiles_IsEmpty",
                table: "OsmFiles",
                column: "IsEmpty");

            migrationBuilder.CreateIndex(
                name: "IX_OsmFiles_IsNodesLoad",
                table: "OsmFiles",
                column: "IsNodesLoad");

            migrationBuilder.CreateIndex(
                name: "IX_OsmFiles_IsTagsLoad",
                table: "OsmFiles",
                column: "IsTagsLoad");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OsmFiles");
        }
    }
}
