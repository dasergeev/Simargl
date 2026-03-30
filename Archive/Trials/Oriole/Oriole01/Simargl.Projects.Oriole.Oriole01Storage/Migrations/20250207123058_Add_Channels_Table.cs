using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Simargl.Projects.Oriole.Oriole01Storage.Migrations
{
    /// <inheritdoc />
    internal partial class Add_Channels_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    Key = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false),
                    AdxlKey = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Channels", x => x.Key);
                    table.UniqueConstraint("AK_Channels_Timestamp_AdxlKey_Index_Frequency", x => new { x.Timestamp, x.AdxlKey, x.Index, x.Frequency });
                    table.ForeignKey(
                        name: "FK_Adxls_Channels",
                        column: x => x.AdxlKey,
                        principalTable: "Adxls",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Channels_AdxlKey",
                table: "Channels",
                column: "AdxlKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Channels");
        }
    }
}
