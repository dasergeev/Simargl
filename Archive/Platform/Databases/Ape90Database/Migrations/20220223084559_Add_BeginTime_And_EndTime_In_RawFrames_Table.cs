using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_BeginTime_And_EndTime_In_RawFrames_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BeginTime",
                table: "RawFrames",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Duration",
                table: "RawFrames",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "RawFrames",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsNormalizedTime",
                table: "RawFrames",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_RawFrames_BeginTime",
                table: "RawFrames",
                column: "BeginTime");

            migrationBuilder.CreateIndex(
                name: "IX_RawFrames_EndTime",
                table: "RawFrames",
                column: "EndTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RawFrames_BeginTime",
                table: "RawFrames");

            migrationBuilder.DropIndex(
                name: "IX_RawFrames_EndTime",
                table: "RawFrames");

            migrationBuilder.DropColumn(
                name: "BeginTime",
                table: "RawFrames");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "RawFrames");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "RawFrames");

            migrationBuilder.DropColumn(
                name: "IsNormalizedTime",
                table: "RawFrames");
        }
    }
}
