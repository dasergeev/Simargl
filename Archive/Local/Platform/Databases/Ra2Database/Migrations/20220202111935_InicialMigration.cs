using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class InicialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RawFiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawFiles", x => x.Id);
                    table.UniqueConstraint("AK_RawFiles_FilePath", x => x.FilePath);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RawFiles_FilePath",
                table: "RawFiles",
                column: "FilePath");

            migrationBuilder.CreateIndex(
                name: "IX_RawFiles_Id",
                table: "RawFiles",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RawFiles");
        }
    }
}
