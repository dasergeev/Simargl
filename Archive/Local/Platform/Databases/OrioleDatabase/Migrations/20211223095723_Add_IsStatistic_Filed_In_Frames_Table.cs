using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_IsStatistic_Filed_In_Frames_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsStatistic",
                table: "Frames",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Frames_IsStatistic",
                table: "Frames",
                column: "IsStatistic");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Frames_IsStatistic",
                table: "Frames");

            migrationBuilder.DropColumn(
                name: "IsStatistic",
                table: "Frames");
        }
    }
}
