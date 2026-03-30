using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_SynchromarkersFields_In_PackageFiles_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FirstSynchromarker",
                table: "PackageFiles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrectSynchromarkersChain",
                table: "PackageFiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "LastSynchromarker",
                table: "PackageFiles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstSynchromarker",
                table: "PackageFiles");

            migrationBuilder.DropColumn(
                name: "IsCorrectSynchromarkersChain",
                table: "PackageFiles");

            migrationBuilder.DropColumn(
                name: "LastSynchromarker",
                table: "PackageFiles");
        }
    }
}
