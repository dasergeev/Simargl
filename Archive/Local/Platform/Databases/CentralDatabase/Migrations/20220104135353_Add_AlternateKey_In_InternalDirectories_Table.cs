using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_AlternateKey_In_InternalDirectories_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_InternalDirectories_FileStorageId_GeneralDirectoryId_Path",
                table: "InternalDirectories",
                columns: new[] { "FileStorageId", "GeneralDirectoryId", "Path" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_InternalDirectories_FileStorageId_GeneralDirectoryId_Path",
                table: "InternalDirectories");
        }
    }
}
