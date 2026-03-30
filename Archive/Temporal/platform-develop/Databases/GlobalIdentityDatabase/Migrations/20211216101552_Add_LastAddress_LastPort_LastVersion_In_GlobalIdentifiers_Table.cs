using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_LastAddress_LastPort_LastVersion_In_GlobalIdentifiers_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastAddress",
                table: "GlobalIdentifiers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LastPort",
                table: "GlobalIdentifiers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LastVersion",
                table: "GlobalIdentifiers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastAddress",
                table: "GlobalIdentifiers");

            migrationBuilder.DropColumn(
                name: "LastPort",
                table: "GlobalIdentifiers");

            migrationBuilder.DropColumn(
                name: "LastVersion",
                table: "GlobalIdentifiers");
        }
    }
}
