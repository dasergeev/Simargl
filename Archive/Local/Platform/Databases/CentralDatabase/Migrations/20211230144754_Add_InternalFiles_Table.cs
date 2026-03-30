using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_InternalFiles_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InternalFiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentDirectoryId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternalFiles_InternalDirectories",
                        column: x => x.ParentDirectoryId,
                        principalTable: "InternalDirectories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InternalFiles_Id",
                table: "InternalFiles",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_InternalFiles_Name",
                table: "InternalFiles",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_InternalFiles_ParentDirectoryId",
                table: "InternalFiles",
                column: "ParentDirectoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternalFiles");
        }
    }
}
