using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    public partial class Add_Fields_BeginTime_EndTime_RawFiles : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BeginTime",
                table: "RawFrames",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

            migrationBuilder.CreateIndex(
                name: "IX_RawFrames_IsNormalizedTime",
                table: "RawFrames",
                column: "IsNormalizedTime");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RawFrames_BeginTime",
                table: "RawFrames");

            migrationBuilder.DropIndex(
                name: "IX_RawFrames_EndTime",
                table: "RawFrames");

            migrationBuilder.DropIndex(
                name: "IX_RawFrames_IsNormalizedTime",
                table: "RawFrames");

            migrationBuilder.DropColumn(
                name: "BeginTime",
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
