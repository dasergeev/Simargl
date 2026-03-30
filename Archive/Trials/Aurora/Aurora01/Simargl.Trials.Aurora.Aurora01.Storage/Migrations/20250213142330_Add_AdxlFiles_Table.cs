using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simargl.Trials.Aurora.Aurora01.Storage.Migrations
{
    /// <inheritdoc />
    internal partial class Add_AdxlFiles_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdxlFiles",
                columns: table => new
                {
                    AdxlAddress = table.Column<int>(type: "integer", nullable: false),
                    HourDirectoryTimestamp = table.Column<int>(type: "integer", nullable: false),
                    Timestamp = table.Column<int>(type: "integer", nullable: false),
                    State = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdxlFiles", x => new { x.AdxlAddress, x.HourDirectoryTimestamp, x.Timestamp });
                    table.ForeignKey(
                        name: "FK_Adxls_AdxlFiles",
                        column: x => x.AdxlAddress,
                        principalTable: "Adxls",
                        principalColumn: "Address",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HourDirectories_AdxlFiles",
                        column: x => x.HourDirectoryTimestamp,
                        principalTable: "HourDirectories",
                        principalColumn: "Timestamp",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdxlFiles_HourDirectoryTimestamp",
                table: "AdxlFiles",
                column: "HourDirectoryTimestamp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdxlFiles");
        }
    }
}
