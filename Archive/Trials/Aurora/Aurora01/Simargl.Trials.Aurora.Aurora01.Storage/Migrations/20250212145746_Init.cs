using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simargl.Trials.Aurora.Aurora01.Storage.Migrations
{
    /// <inheritdoc />
    internal partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HourDirectories",
                columns: table => new
                {
                    Timestamp = table.Column<int>(type: "integer", nullable: false),
                    State = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HourDirectories", x => x.Timestamp);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HourDirectories");
        }
    }
}
