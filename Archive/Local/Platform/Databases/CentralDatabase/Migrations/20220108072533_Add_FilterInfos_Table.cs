using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_FilterInfos_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FilterInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LowerFrequency = table.Column<double>(type: "float", nullable: false),
                    UpperFrequency = table.Column<double>(type: "float", nullable: false),
                    IsInverted = table.Column<bool>(type: "bit", nullable: false),
                    FormatId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterInfos", x => x.Id);
                    table.UniqueConstraint("AK_FilterInfos_LowerFrequency_UpperFrequency_IsInverted_FormatId", x => new { x.LowerFrequency, x.UpperFrequency, x.IsInverted, x.FormatId });
                    table.ForeignKey(
                        name: "FK_FilterInfos_FilterFormats",
                        column: x => x.FormatId,
                        principalTable: "FilterFormats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilterInfos_FormatId",
                table: "FilterInfos",
                column: "FormatId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterInfos_Id",
                table: "FilterInfos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FilterInfos_IsInverted",
                table: "FilterInfos",
                column: "IsInverted");

            migrationBuilder.CreateIndex(
                name: "IX_FilterInfos_LowerFrequency",
                table: "FilterInfos",
                column: "LowerFrequency");

            migrationBuilder.CreateIndex(
                name: "IX_FilterInfos_UpperFrequency",
                table: "FilterInfos",
                column: "UpperFrequency");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilterInfos");
        }
    }
}
