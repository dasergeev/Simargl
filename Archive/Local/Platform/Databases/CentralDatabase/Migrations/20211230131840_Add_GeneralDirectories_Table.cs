using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_GeneralDirectories_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneralDirectories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileStorageId = table.Column<long>(type: "bigint", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralDirectories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneralDirectories_FileStorages",
                        column: x => x.FileStorageId,
                        principalTable: "FileStorages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeneralDirectories_FileStorageId",
                table: "GeneralDirectories",
                column: "FileStorageId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralDirectories_Id",
                table: "GeneralDirectories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralDirectories_Name",
                table: "GeneralDirectories",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneralDirectories");
        }
    }
}
