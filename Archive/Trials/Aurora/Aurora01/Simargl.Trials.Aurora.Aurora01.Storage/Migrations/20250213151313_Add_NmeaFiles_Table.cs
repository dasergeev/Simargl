using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simargl.Trials.Aurora.Aurora01.Storage.Migrations
{
    /// <inheritdoc />
    internal partial class Add_NmeaFiles_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NmeaFiles",
                columns: table => new
                {
                    HourDirectoryTimestamp = table.Column<int>(type: "integer", nullable: false),
                    Minute = table.Column<byte>(type: "smallint", nullable: false),
                    State = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NmeaFiles", x => new { x.HourDirectoryTimestamp, x.Minute });
                    table.ForeignKey(
                        name: "FK_HourDirectories_NmeaFiles",
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
                name: "NmeaFiles");
        }
    }
}
