using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_DatabaseTableCategories_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DatabaseTableCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatabaseTableCategories", x => x.Id);
                    table.UniqueConstraint("AK_DatabaseTableCategories_Name", x => x.Name);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DatabaseTableCategories_Id",
                table: "DatabaseTableCategories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DatabaseTableCategories_Name",
                table: "DatabaseTableCategories",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatabaseTableCategories");
        }
    }
}
