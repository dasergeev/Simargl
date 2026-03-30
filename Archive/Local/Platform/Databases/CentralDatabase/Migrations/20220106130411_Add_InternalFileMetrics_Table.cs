using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_InternalFileMetrics_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InternalFileMetrics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsExists = table.Column<bool>(type: "bit", nullable: false),
                    DeterminationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastAccessTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastWriteTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    Crc32 = table.Column<byte>(type: "tinyint", nullable: false),
                    HashCode = table.Column<int>(type: "int", nullable: false),
                    BytesSum = table.Column<int>(type: "int", nullable: false),
                    FileId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalFileMetrics", x => x.Id);
                    table.UniqueConstraint("AK_InternalFileMetrics_FileId_DeterminationTime", x => new { x.FileId, x.DeterminationTime });
                    table.ForeignKey(
                        name: "FK_InternalFileMetrics_InternalFiles",
                        column: x => x.FileId,
                        principalTable: "InternalFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InternalFileMetrics_CreationTime",
                table: "InternalFileMetrics",
                column: "CreationTime");

            migrationBuilder.CreateIndex(
                name: "IX_InternalFileMetrics_DeterminationTime",
                table: "InternalFileMetrics",
                column: "DeterminationTime");

            migrationBuilder.CreateIndex(
                name: "IX_InternalFileMetrics_FileId",
                table: "InternalFileMetrics",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalFileMetrics_Id",
                table: "InternalFileMetrics",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_InternalFileMetrics_IsExists",
                table: "InternalFileMetrics",
                column: "IsExists");

            migrationBuilder.CreateIndex(
                name: "IX_InternalFileMetrics_LastAccessTime",
                table: "InternalFileMetrics",
                column: "LastAccessTime");

            migrationBuilder.CreateIndex(
                name: "IX_InternalFileMetrics_LastWriteTime",
                table: "InternalFileMetrics",
                column: "LastWriteTime");

            migrationBuilder.CreateIndex(
                name: "IX_InternalFileMetrics_RegistrationTime",
                table: "InternalFileMetrics",
                column: "RegistrationTime");

            migrationBuilder.CreateIndex(
                name: "IX_InternalFileMetrics_Size",
                table: "InternalFileMetrics",
                column: "Size");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternalFileMetrics");
        }
    }
}
