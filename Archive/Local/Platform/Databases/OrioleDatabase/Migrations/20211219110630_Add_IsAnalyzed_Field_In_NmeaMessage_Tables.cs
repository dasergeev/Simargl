using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_IsAnalyzed_Field_In_NmeaMessage_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAnalyzed",
                table: "VtgMessages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAnalyzed",
                table: "RmcMessages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAnalyzed",
                table: "GgaMessages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_VtgMessages_IsAnalyzed",
                table: "VtgMessages",
                column: "IsAnalyzed");

            migrationBuilder.CreateIndex(
                name: "IX_RmcMessages_IsAnalyzed",
                table: "RmcMessages",
                column: "IsAnalyzed");

            migrationBuilder.CreateIndex(
                name: "IX_GgaMessages_IsAnalyzed",
                table: "GgaMessages",
                column: "IsAnalyzed");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VtgMessages_IsAnalyzed",
                table: "VtgMessages");

            migrationBuilder.DropIndex(
                name: "IX_RmcMessages_IsAnalyzed",
                table: "RmcMessages");

            migrationBuilder.DropIndex(
                name: "IX_GgaMessages_IsAnalyzed",
                table: "GgaMessages");

            migrationBuilder.DropColumn(
                name: "IsAnalyzed",
                table: "VtgMessages");

            migrationBuilder.DropColumn(
                name: "IsAnalyzed",
                table: "RmcMessages");

            migrationBuilder.DropColumn(
                name: "IsAnalyzed",
                table: "GgaMessages");
        }
    }
}
