using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_Locations_Fields_In_Statistics_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Statistics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Statistics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_Latitude",
                table: "Statistics",
                column: "Latitude");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_Latitude_Longitude",
                table: "Statistics",
                columns: new[] { "Latitude", "Longitude" });

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_Longitude",
                table: "Statistics",
                column: "Longitude");

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_Locations",
                table: "Statistics",
                columns: new[] { "Latitude", "Longitude" },
                principalTable: "Locations",
                principalColumns: new[] { "Latitude", "Longitude" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_Locations",
                table: "Statistics");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_Latitude",
                table: "Statistics");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_Latitude_Longitude",
                table: "Statistics");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_Longitude",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Statistics");
        }
    }
}
