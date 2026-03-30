using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_InternalDirectories_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InternalDirectories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GeneralDirectoryId = table.Column<long>(type: "bigint", nullable: false),
                    ParentDirectoryId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalDirectories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternalDirectories_GeneralDirectories",
                        column: x => x.GeneralDirectoryId,
                        principalTable: "GeneralDirectories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InternalDirectories_InternalDirectories",
                        column: x => x.ParentDirectoryId,
                        principalTable: "InternalDirectories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InternalDirectories_GeneralDirectoryId",
                table: "InternalDirectories",
                column: "GeneralDirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalDirectories_Id",
                table: "InternalDirectories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_InternalDirectories_Name",
                table: "InternalDirectories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_InternalDirectories_ParentDirectoryId",
                table: "InternalDirectories",
                column: "ParentDirectoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternalDirectories");
        }
    }
}
