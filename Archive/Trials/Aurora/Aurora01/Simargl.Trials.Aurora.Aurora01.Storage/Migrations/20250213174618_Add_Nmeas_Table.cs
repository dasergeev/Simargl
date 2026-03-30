using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simargl.Trials.Aurora.Aurora01.Storage.Migrations
{
    /// <inheritdoc />
    internal partial class Add_Nmeas_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nmeas",
                columns: table => new
                {
                    Timestamp = table.Column<long>(type: "bigint", nullable: false),
                    Speed = table.Column<double>(type: "double precision", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nmeas", x => x.Timestamp);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nmeas");
        }
    }
}
