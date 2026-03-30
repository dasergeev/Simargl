using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Simargl.Projects.Oriole.Oriole01Storage.Migrations
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
                    Key = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false),
                    HourDirectoryKey = table.Column<long>(type: "bigint", nullable: false),
                    AdxlKey = table.Column<long>(type: "bigint", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdxlFiles", x => x.Key);
                    table.UniqueConstraint("AK_AdxlFiles_Timestamp_AdxlKey", x => new { x.Timestamp, x.AdxlKey });
                    table.ForeignKey(
                        name: "FK_Adxls_AdxlFiles",
                        column: x => x.AdxlKey,
                        principalTable: "Adxls",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HourDirectories_AdxlFiles",
                        column: x => x.HourDirectoryKey,
                        principalTable: "HourDirectories",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdxlFiles_AdxlKey",
                table: "AdxlFiles",
                column: "AdxlKey");

            migrationBuilder.CreateIndex(
                name: "IX_AdxlFiles_HourDirectoryKey",
                table: "AdxlFiles",
                column: "HourDirectoryKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdxlFiles");
        }
    }
}
