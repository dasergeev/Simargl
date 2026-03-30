using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Simargl.Border.Storage.Migrations
{
    /// <inheritdoc />
    internal partial class Add_Main_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AxesCommits",
                table: "Passages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AxesCount",
                table: "Passages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Axes",
                columns: table => new
                {
                    Key = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Index = table.Column<int>(type: "integer", nullable: false),
                    PassageKey = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Axes", x => x.Key);
                    table.ForeignKey(
                        name: "FK_Passages_Axes",
                        column: x => x.PassageKey,
                        principalTable: "Passages",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AxisInteractions",
                columns: table => new
                {
                    Key = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Section = table.Column<int>(type: "integer", nullable: false),
                    Position = table.Column<double>(type: "double precision", nullable: false),
                    Time = table.Column<double>(type: "double precision", nullable: false),
                    Speed = table.Column<double>(type: "double precision", nullable: false),
                    Length = table.Column<int>(type: "integer", nullable: false),
                    SpeedSum = table.Column<double>(type: "double precision", nullable: false),
                    SpeedSquaresSum = table.Column<double>(type: "double precision", nullable: false),
                    SpeedAverage = table.Column<double>(type: "double precision", nullable: false),
                    SpeedDeviation = table.Column<double>(type: "double precision", nullable: false),
                    LeftSum = table.Column<double>(type: "double precision", nullable: false),
                    LeftSquaresSum = table.Column<double>(type: "double precision", nullable: false),
                    LeftAverage = table.Column<double>(type: "double precision", nullable: false),
                    LeftDeviation = table.Column<double>(type: "double precision", nullable: false),
                    LeftMax = table.Column<double>(type: "double precision", nullable: false),
                    RightSum = table.Column<double>(type: "double precision", nullable: false),
                    RightSquaresSum = table.Column<double>(type: "double precision", nullable: false),
                    RightMax = table.Column<double>(type: "double precision", nullable: false),
                    RightAverage = table.Column<double>(type: "double precision", nullable: false),
                    RightDeviation = table.Column<double>(type: "double precision", nullable: false),
                    AxisKey = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AxisInteractions", x => x.Key);
                    table.ForeignKey(
                        name: "FK_Axes_Interactions",
                        column: x => x.AxisKey,
                        principalTable: "Axes",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Passages_State",
                table: "Passages",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_Axes_PassageKey",
                table: "Axes",
                column: "PassageKey");

            migrationBuilder.CreateIndex(
                name: "IX_AxisInteractions_AxisKey",
                table: "AxisInteractions",
                column: "AxisKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AxisInteractions");

            migrationBuilder.DropTable(
                name: "Axes");

            migrationBuilder.DropIndex(
                name: "IX_Passages_State",
                table: "Passages");

            migrationBuilder.DropColumn(
                name: "AxesCommits",
                table: "Passages");

            migrationBuilder.DropColumn(
                name: "AxesCount",
                table: "Passages");
        }
    }
}
