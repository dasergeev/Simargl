using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_ChannelStatistics_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChannelStatistics",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<long>(type: "bigint", nullable: false),
                    Min = table.Column<double>(type: "float", nullable: false),
                    Max = table.Column<double>(type: "float", nullable: false),
                    Average = table.Column<double>(type: "float", nullable: false),
                    Deviation = table.Column<double>(type: "float", nullable: false),
                    Sum = table.Column<double>(type: "float", nullable: false),
                    SquaresSum = table.Column<double>(type: "float", nullable: false),
                    MinModulo = table.Column<double>(type: "float", nullable: false),
                    MaxModulo = table.Column<double>(type: "float", nullable: false),
                    AverageModulo = table.Column<double>(type: "float", nullable: false),
                    DeviationModulo = table.Column<double>(type: "float", nullable: false),
                    SumModulo = table.Column<double>(type: "float", nullable: false),
                    ChannelId = table.Column<long>(type: "bigint", nullable: false),
                    FilterId = table.Column<long>(type: "bigint", nullable: false),
                    FileMetricId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChannelStatistics_ChannelInfos",
                        column: x => x.ChannelId,
                        principalTable: "ChannelInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChannelStatistics_FilterInfos",
                        column: x => x.FilterId,
                        principalTable: "FilterInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChannelStatistics_InternalFileMetrics",
                        column: x => x.FileMetricId,
                        principalTable: "InternalFileMetrics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChannelStatistics_Average",
                table: "ChannelStatistics",
                column: "Average");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelStatistics_AverageModulo",
                table: "ChannelStatistics",
                column: "AverageModulo");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelStatistics_ChannelId",
                table: "ChannelStatistics",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelStatistics_Count",
                table: "ChannelStatistics",
                column: "Count");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelStatistics_Deviation",
                table: "ChannelStatistics",
                column: "Deviation");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelStatistics_DeviationModulo",
                table: "ChannelStatistics",
                column: "DeviationModulo");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelStatistics_FileMetricId",
                table: "ChannelStatistics",
                column: "FileMetricId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelStatistics_FilterId",
                table: "ChannelStatistics",
                column: "FilterId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelStatistics_Id",
                table: "ChannelStatistics",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelStatistics_Max",
                table: "ChannelStatistics",
                column: "Max");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelStatistics_MaxModulo",
                table: "ChannelStatistics",
                column: "MaxModulo");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelStatistics_Min",
                table: "ChannelStatistics",
                column: "Min");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelStatistics_MinModulo",
                table: "ChannelStatistics",
                column: "MinModulo");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelStatistics_SquaresSum",
                table: "ChannelStatistics",
                column: "SquaresSum");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelStatistics_Sum",
                table: "ChannelStatistics",
                column: "Sum");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelStatistics_SumModulo",
                table: "ChannelStatistics",
                column: "SumModulo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChannelStatistics");
        }
    }
}
