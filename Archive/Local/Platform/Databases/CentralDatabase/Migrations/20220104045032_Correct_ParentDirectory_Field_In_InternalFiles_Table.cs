using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Correct_ParentDirectory_Field_In_InternalFiles_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternalFiles_InternalDirectories",
                table: "InternalFiles");

            migrationBuilder.AddForeignKey(
                name: "FK_InternalFiles_InternalDirectories",
                table: "InternalFiles",
                column: "ParentDirectoryId",
                principalTable: "InternalDirectories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternalFiles_InternalDirectories",
                table: "InternalFiles");

            migrationBuilder.AddForeignKey(
                name: "FK_InternalFiles_InternalDirectories",
                table: "InternalFiles",
                column: "ParentDirectoryId",
                principalTable: "InternalDirectories",
                principalColumn: "Id");
        }
    }
}
