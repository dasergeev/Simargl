using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class AddFields_IsAnalized : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAnalyzed",
                table: "RawFiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_RawFiles_IsAnalyzed",
                table: "RawFiles",
                column: "IsAnalyzed");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RawFiles_IsAnalyzed",
                table: "RawFiles");

            migrationBuilder.DropColumn(
                name: "IsAnalyzed",
                table: "RawFiles");
        }
    }
}
