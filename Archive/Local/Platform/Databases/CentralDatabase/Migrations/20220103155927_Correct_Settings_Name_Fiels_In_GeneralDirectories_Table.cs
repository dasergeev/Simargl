using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Correct_Settings_Name_Fiels_In_GeneralDirectories_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_GeneralDirectories_Name",
                table: "GeneralDirectories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_GeneralDirectories_Name",
                table: "GeneralDirectories",
                column: "Name");
        }
    }
}
