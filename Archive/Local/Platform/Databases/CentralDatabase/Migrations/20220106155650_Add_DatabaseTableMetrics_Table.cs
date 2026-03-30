using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_DatabaseTableMetrics_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DatabaseTableMetrics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeterminationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Count = table.Column<long>(type: "bigint", nullable: false),
                    TableId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatabaseTableMetrics", x => x.Id);
                    table.UniqueConstraint("AK_DatabaseTableMetrics_TableId_DeterminationTime", x => new { x.TableId, x.DeterminationTime });
                    table.ForeignKey(
                        name: "FK_DatabaseTableMetrics_DatabaseTables",
                        column: x => x.TableId,
                        principalTable: "DatabaseTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DatabaseTableMetrics_Count",
                table: "DatabaseTableMetrics",
                column: "Count");

            migrationBuilder.CreateIndex(
                name: "IX_DatabaseTableMetrics_DeterminationTime",
                table: "DatabaseTableMetrics",
                column: "DeterminationTime");

            migrationBuilder.CreateIndex(
                name: "IX_DatabaseTableMetrics_Id",
                table: "DatabaseTableMetrics",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DatabaseTableMetrics_TableId",
                table: "DatabaseTableMetrics",
                column: "TableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatabaseTableMetrics");
        }
    }
}
