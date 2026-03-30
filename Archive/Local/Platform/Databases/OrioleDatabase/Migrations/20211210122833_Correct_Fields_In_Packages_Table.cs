using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Correct_Fields_In_Packages_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Packages_StartTime",
                table: "Packages");

            migrationBuilder.RenameColumn(
                name: "SensorNumber",
                table: "Packages",
                newName: "Format");

            migrationBuilder.RenameColumn(
                name: "RegistrarId",
                table: "Packages",
                newName: "RawDirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_Synchromarker",
                table: "Packages",
                column: "Synchromarker");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_PackageFiles",
                table: "Packages",
                columns: new[] { "RawDirectoryId", "Format", "FileTime" },
                principalTable: "PackageFiles",
                principalColumns: new[] { "RawDirectoryId", "Format", "Time" });

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_RawDirectories",
                table: "Packages",
                column: "RawDirectoryId",
                principalTable: "RawDirectories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_PackageFiles",
                table: "Packages");

            migrationBuilder.DropForeignKey(
                name: "FK_Packages_RawDirectories",
                table: "Packages");

            migrationBuilder.DropIndex(
                name: "IX_Packages_Synchromarker",
                table: "Packages");

            migrationBuilder.RenameColumn(
                name: "Format",
                table: "Packages",
                newName: "SensorNumber");

            migrationBuilder.RenameColumn(
                name: "RawDirectoryId",
                table: "Packages",
                newName: "RegistrarId");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_StartTime",
                table: "Packages",
                column: "StartTime");
        }
    }
}
