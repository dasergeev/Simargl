using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Chenged_Time_Field_In_PackageFiles_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCorrectSynchromarkersChain",
                table: "PackageFiles");

            migrationBuilder.RenameColumn(
                name: "NormalizedTime",
                table: "PackageFiles",
                newName: "NormalizedEndTime");

            migrationBuilder.RenameIndex(
                name: "IX_PackageFiles_NormalizedTime",
                table: "PackageFiles",
                newName: "IX_PackageFiles_NormalizedEndTime");

            migrationBuilder.AddColumn<int>(
                name: "LocationType",
                table: "PackageFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "NormalizedBeginTime",
                table: "PackageFiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_PackageFiles_LocationType",
                table: "PackageFiles",
                column: "LocationType");

            migrationBuilder.CreateIndex(
                name: "IX_PackageFiles_NormalizedBeginTime",
                table: "PackageFiles",
                column: "NormalizedBeginTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PackageFiles_LocationType",
                table: "PackageFiles");

            migrationBuilder.DropIndex(
                name: "IX_PackageFiles_NormalizedBeginTime",
                table: "PackageFiles");

            migrationBuilder.DropColumn(
                name: "LocationType",
                table: "PackageFiles");

            migrationBuilder.DropColumn(
                name: "NormalizedBeginTime",
                table: "PackageFiles");

            migrationBuilder.RenameColumn(
                name: "NormalizedEndTime",
                table: "PackageFiles",
                newName: "NormalizedTime");

            migrationBuilder.RenameIndex(
                name: "IX_PackageFiles_NormalizedEndTime",
                table: "PackageFiles",
                newName: "IX_PackageFiles_NormalizedTime");

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrectSynchromarkersChain",
                table: "PackageFiles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
