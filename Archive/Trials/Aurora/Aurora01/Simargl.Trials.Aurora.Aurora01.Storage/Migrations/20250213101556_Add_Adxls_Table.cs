using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simargl.Trials.Aurora.Aurora01.Storage.Migrations
{
    /// <inheritdoc />
    internal partial class Add_Adxls_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adxls",
                columns: table => new
                {
                    Address = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adxls", x => x.Address);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adxls");
        }
    }
}
