using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_IsLoaded_In_PackageFiles_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLoaded",
                table: "PackageFiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_PackageFiles_IsLoaded",
                table: "PackageFiles",
                column: "IsLoaded");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PackageFiles_IsLoaded",
                table: "PackageFiles");

            migrationBuilder.DropColumn(
                name: "IsLoaded",
                table: "PackageFiles");
        }
    }
}
