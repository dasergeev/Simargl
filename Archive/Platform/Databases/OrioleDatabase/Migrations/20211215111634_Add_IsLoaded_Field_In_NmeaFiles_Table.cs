using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_IsLoaded_Field_In_NmeaFiles_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLoaded",
                table: "NmeaFile",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_NmeaFile_IsLoaded",
                table: "NmeaFile",
                column: "IsLoaded");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NmeaFile_IsLoaded",
                table: "NmeaFile");

            migrationBuilder.DropColumn(
                name: "IsLoaded",
                table: "NmeaFile");
        }
    }
}
