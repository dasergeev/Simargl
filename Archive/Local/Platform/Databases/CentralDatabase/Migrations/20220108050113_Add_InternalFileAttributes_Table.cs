using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_InternalFileAttributes_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InternalFileAttributes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<long>(type: "bigint", nullable: false),
                    FormatId = table.Column<long>(type: "bigint", nullable: false),
                    FileId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalFileAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternalFileAttributes_InternalFileAttributeFormats",
                        column: x => x.FormatId,
                        principalTable: "InternalFileAttributeFormats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InternalFileAttributes_InternalFiles",
                        column: x => x.FileId,
                        principalTable: "InternalFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InternalFileAttributes_FileId",
                table: "InternalFileAttributes",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalFileAttributes_FormatId",
                table: "InternalFileAttributes",
                column: "FormatId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalFileAttributes_Id",
                table: "InternalFileAttributes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_InternalFileAttributes_Value",
                table: "InternalFileAttributes",
                column: "Value");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternalFileAttributes");
        }
    }
}
