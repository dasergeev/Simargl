using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Correct_Relation_Statistics_Filters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_Filters",
                table: "Statistics");

            migrationBuilder.DropIndex(
                name: "IX_Statistics__FilterCutoff",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "_FilterCutoff",
                table: "Statistics");

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_Filters",
                table: "Statistics",
                column: "Cutoff",
                principalTable: "Filters",
                principalColumn: "Cutoff",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_Filters",
                table: "Statistics");

            migrationBuilder.AddColumn<double>(
                name: "_FilterCutoff",
                table: "Statistics",
                type: "float",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Statistics__FilterCutoff",
                table: "Statistics",
                column: "_FilterCutoff");

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_Filters",
                table: "Statistics",
                column: "_FilterCutoff",
                principalTable: "Filters",
                principalColumn: "Cutoff",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
