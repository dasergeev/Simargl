using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_InternalFileAttributeFormats_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InternalFileAttributeFormats",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalFileAttributeFormats", x => x.Id);
                    table.UniqueConstraint("AK_InternalFileAttributeFormats_Name", x => x.Name);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InternalFileAttributeFormats_Id",
                table: "InternalFileAttributeFormats",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_InternalFileAttributeFormats_Name",
                table: "InternalFileAttributeFormats",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternalFileAttributeFormats");
        }
    }
}
