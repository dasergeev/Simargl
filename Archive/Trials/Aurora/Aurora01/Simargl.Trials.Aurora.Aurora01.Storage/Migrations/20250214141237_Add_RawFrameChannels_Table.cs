using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simargl.Trials.Aurora.Aurora01.Storage.Migrations
{
    /// <inheritdoc />
    internal partial class Add_RawFrameChannels_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RawFrameChannels",
                columns: table => new
                {
                    Timestamp = table.Column<long>(type: "bigint", nullable: false),
                    Index = table.Column<int>(type: "integer", nullable: false),
                    Frequency = table.Column<double>(type: "double precision", nullable: false),
                    Sum = table.Column<double>(type: "double precision", nullable: false),
                    SquaresSum = table.Column<double>(type: "double precision", nullable: false),
                    Min = table.Column<double>(type: "double precision", nullable: false),
                    Max = table.Column<double>(type: "double precision", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawFrameChannels", x => new { x.Timestamp, x.Index, x.Frequency });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RawFrameChannels");
        }
    }
}
