using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_Path_And_FileStorage_Fields_In_InternalDirectories_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FileStorageId",
                table: "InternalDirectories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "InternalDirectories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_InternalDirectories_FileStorageId",
                table: "InternalDirectories",
                column: "FileStorageId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalDirectories_Path",
                table: "InternalDirectories",
                column: "Path");

            migrationBuilder.AddForeignKey(
                name: "FK_InternalDirectories_FileStorages",
                table: "InternalDirectories",
                column: "FileStorageId",
                principalTable: "FileStorages",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternalDirectories_FileStorages",
                table: "InternalDirectories");

            migrationBuilder.DropIndex(
                name: "IX_InternalDirectories_FileStorageId",
                table: "InternalDirectories");

            migrationBuilder.DropIndex(
                name: "IX_InternalDirectories_Path",
                table: "InternalDirectories");

            migrationBuilder.DropColumn(
                name: "FileStorageId",
                table: "InternalDirectories");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "InternalDirectories");
        }
    }
}
