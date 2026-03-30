using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_Frames_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Frames",
                columns: table => new
                {
                    RegistrarId = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false),
                    IsAnalyzed = table.Column<bool>(type: "bit", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frames", x => new { x.RegistrarId, x.Timestamp });
                    table.ForeignKey(
                        name: "FK_Frames_Registrars",
                        column: x => x.RegistrarId,
                        principalTable: "Registrars",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Frames_IsAnalyzed",
                table: "Frames",
                column: "IsAnalyzed");

            migrationBuilder.CreateIndex(
                name: "IX_Frames_RegistrarId",
                table: "Frames",
                column: "RegistrarId");

            migrationBuilder.CreateIndex(
                name: "IX_Frames_Time",
                table: "Frames",
                column: "Time");

            migrationBuilder.CreateIndex(
                name: "IX_Frames_Timestamp",
                table: "Frames",
                column: "Timestamp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Frames");
        }
    }
}
