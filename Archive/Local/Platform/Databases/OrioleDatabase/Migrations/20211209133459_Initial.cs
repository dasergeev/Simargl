using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    RegistrarId = table.Column<int>(type: "int", nullable: false),
                    SensorNumber = table.Column<int>(type: "int", nullable: false),
                    FileTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileOffset = table.Column<int>(type: "int", nullable: false),
                    IsAnalyzed = table.Column<bool>(type: "bit", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Synchromarker = table.Column<long>(type: "bigint", nullable: false),
                    Sampling = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => new { x.RegistrarId, x.SensorNumber, x.FileTime, x.FileOffset });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Packages_IsAnalyzed",
                table: "Packages",
                column: "IsAnalyzed");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_StartTime",
                table: "Packages",
                column: "StartTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Packages");
        }
    }
}
