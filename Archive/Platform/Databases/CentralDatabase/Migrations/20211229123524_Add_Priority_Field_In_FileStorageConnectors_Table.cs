using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_Priority_Field_In_FileStorageConnectors_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "FileStorageConnectors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FileStorageConnectors_Priority",
                table: "FileStorageConnectors",
                column: "Priority");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FileStorageConnectors_Priority",
                table: "FileStorageConnectors");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "FileStorageConnectors");
        }
    }
}
