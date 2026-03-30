using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_PackageFiles_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PackageFiles",
                columns: table => new
                {
                    RawDirectoryId = table.Column<int>(type: "int", nullable: false),
                    Format = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAnalyzed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageFiles", x => new { x.RawDirectoryId, x.Format, x.Time });
                    table.ForeignKey(
                        name: "FK_PackageFiles_RawDirectories",
                        column: x => x.RawDirectoryId,
                        principalTable: "RawDirectories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PackageFiles_IsAnalyzed",
                table: "PackageFiles",
                column: "IsAnalyzed");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackageFiles");
        }
    }
}
