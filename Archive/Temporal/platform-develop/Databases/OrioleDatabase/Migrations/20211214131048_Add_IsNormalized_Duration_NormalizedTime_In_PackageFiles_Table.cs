using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_IsNormalized_Duration_NormalizedTime_In_PackageFiles_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "PackageFiles",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<bool>(
                name: "IsNormalized",
                table: "PackageFiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "NormalizedTime",
                table: "PackageFiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_PackageFiles_IsNormalized",
                table: "PackageFiles",
                column: "IsNormalized");

            migrationBuilder.CreateIndex(
                name: "IX_PackageFiles_NormalizedTime",
                table: "PackageFiles",
                column: "NormalizedTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PackageFiles_IsNormalized",
                table: "PackageFiles");

            migrationBuilder.DropIndex(
                name: "IX_PackageFiles_NormalizedTime",
                table: "PackageFiles");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "PackageFiles");

            migrationBuilder.DropColumn(
                name: "IsNormalized",
                table: "PackageFiles");

            migrationBuilder.DropColumn(
                name: "NormalizedTime",
                table: "PackageFiles");
        }
    }
}
