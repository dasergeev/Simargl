using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simargl.Trials.Aurora.Aurora01.Storage.Migrations
{
    /// <inheritdoc />
    internal partial class Add_AdxlChannels_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdxlChannels",
                columns: table => new
                {
                    Timestamp = table.Column<long>(type: "bigint", nullable: false),
                    AdxlAddress = table.Column<int>(type: "integer", nullable: false),
                    Index = table.Column<byte>(type: "smallint", nullable: false),
                    Frequency = table.Column<double>(type: "double precision", nullable: false),
                    Sum = table.Column<double>(type: "double precision", nullable: false),
                    SquaresSum = table.Column<double>(type: "double precision", nullable: false),
                    Min = table.Column<double>(type: "double precision", nullable: false),
                    Max = table.Column<double>(type: "double precision", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdxlChannels", x => new { x.Timestamp, x.AdxlAddress, x.Index, x.Frequency });
                    table.ForeignKey(
                        name: "FK_Adxls_AdxlChannels",
                        column: x => x.AdxlAddress,
                        principalTable: "Adxls",
                        principalColumn: "Address",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdxlChannels_AdxlAddress",
                table: "AdxlChannels",
                column: "AdxlAddress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdxlChannels");
        }
    }
}
