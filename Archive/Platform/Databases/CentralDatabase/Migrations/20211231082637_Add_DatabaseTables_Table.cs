using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_DatabaseTables_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DatabaseTables",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatabaseTables", x => x.Id);
                    table.UniqueConstraint("AK_DatabaseTables_Name", x => x.Name);
                    table.ForeignKey(
                        name: "FK_DatabaseTables_DatabaseTableCategories",
                        column: x => x.CategoryId,
                        principalTable: "DatabaseTableCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DatabaseTables_CategoryId",
                table: "DatabaseTables",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DatabaseTables_Id",
                table: "DatabaseTables",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DatabaseTables_Name",
                table: "DatabaseTables",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatabaseTables");
        }
    }
}
