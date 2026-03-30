using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simargl.Trials.Aurora.Aurora01.Storage.Migrations
{
    /// <inheritdoc />
    internal partial class Add_RawFrameFiles_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RawFrameFiles",
                columns: table => new
                {
                    HourDirectoryTimestamp = table.Column<int>(type: "integer", nullable: false),
                    Timestamp = table.Column<int>(type: "integer", nullable: false),
                    State = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawFrameFiles", x => new { x.HourDirectoryTimestamp, x.Timestamp });
                    table.ForeignKey(
                        name: "FK_HourDirectories_RawFrameFiles",
                        column: x => x.HourDirectoryTimestamp,
                        principalTable: "HourDirectories",
                        principalColumn: "Timestamp",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RawFrameFiles");
        }
    }
}
