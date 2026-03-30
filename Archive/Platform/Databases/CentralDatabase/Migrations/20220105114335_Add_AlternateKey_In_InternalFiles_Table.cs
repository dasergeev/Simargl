using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_AlternateKey_In_InternalFiles_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FileStorageId",
                table: "InternalFiles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "GeneralDirectoryId",
                table: "InternalFiles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "InternalFiles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationTime",
                table: "InternalFiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_InternalFiles_FileStorageId_GeneralDirectoryId_Path",
                table: "InternalFiles",
                columns: new[] { "FileStorageId", "GeneralDirectoryId", "Path" });

            migrationBuilder.CreateIndex(
                name: "IX_InternalFiles_FileStorageId",
                table: "InternalFiles",
                column: "FileStorageId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalFiles_GeneralDirectoryId",
                table: "InternalFiles",
                column: "GeneralDirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalFiles_Path",
                table: "InternalFiles",
                column: "Path");

            migrationBuilder.CreateIndex(
                name: "IX_InternalFiles_RegistrationTime",
                table: "InternalFiles",
                column: "RegistrationTime");

            migrationBuilder.AddForeignKey(
                name: "FK_InternalFiles_FileStorages",
                table: "InternalFiles",
                column: "FileStorageId",
                principalTable: "FileStorages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InternalFiles_GeneralDirectories",
                table: "InternalFiles",
                column: "GeneralDirectoryId",
                principalTable: "GeneralDirectories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternalFiles_FileStorages",
                table: "InternalFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_InternalFiles_GeneralDirectories",
                table: "InternalFiles");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_InternalFiles_FileStorageId_GeneralDirectoryId_Path",
                table: "InternalFiles");

            migrationBuilder.DropIndex(
                name: "IX_InternalFiles_FileStorageId",
                table: "InternalFiles");

            migrationBuilder.DropIndex(
                name: "IX_InternalFiles_GeneralDirectoryId",
                table: "InternalFiles");

            migrationBuilder.DropIndex(
                name: "IX_InternalFiles_Path",
                table: "InternalFiles");

            migrationBuilder.DropIndex(
                name: "IX_InternalFiles_RegistrationTime",
                table: "InternalFiles");

            migrationBuilder.DropColumn(
                name: "FileStorageId",
                table: "InternalFiles");

            migrationBuilder.DropColumn(
                name: "GeneralDirectoryId",
                table: "InternalFiles");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "InternalFiles");

            migrationBuilder.DropColumn(
                name: "RegistrationTime",
                table: "InternalFiles");
        }
    }
}
