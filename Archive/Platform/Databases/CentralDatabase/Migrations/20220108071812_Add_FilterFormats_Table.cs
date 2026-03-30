using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_FilterFormats_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FilterFormats",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterFormats", x => x.Id);
                    table.UniqueConstraint("AK_FilterFormats_Name", x => x.Name);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilterFormats_Id",
                table: "FilterFormats",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FilterFormats_Name",
                table: "FilterFormats",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilterFormats");
        }
    }
}
