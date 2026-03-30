using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_ChannelDistributions_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DistributionsCount",
                table: "ChannelInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ChannelDistributions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<long>(type: "bigint", nullable: false),
                    ChannelId = table.Column<long>(type: "bigint", nullable: false),
                    FilterId = table.Column<long>(type: "bigint", nullable: false),
                    FileMetricId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelDistributions", x => x.Id);
                    table.UniqueConstraint("AK_ChannelDistributions_ChannelId_FilterId_FileMetricId", x => new { x.ChannelId, x.FilterId, x.FileMetricId });
                    table.ForeignKey(
                        name: "FK_ChannelDistributions_ChannelInfos",
                        column: x => x.ChannelId,
                        principalTable: "ChannelInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChannelDistributions_FilterInfos",
                        column: x => x.FilterId,
                        principalTable: "FilterInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChannelDistributions_InternalFileMetrics",
                        column: x => x.FileMetricId,
                        principalTable: "InternalFileMetrics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChannelDistributionValues",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<double>(type: "float", nullable: false),
                    Count = table.Column<long>(type: "bigint", nullable: false),
                    DistributionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelDistributionValues", x => x.Id);
                    table.UniqueConstraint("AK_ChannelDistributionValues_DistributionId_Value", x => new { x.DistributionId, x.Value });
                    table.ForeignKey(
                        name: "FK_ChannelDistributionValues_ChannelDistributions",
                        column: x => x.DistributionId,
                        principalTable: "ChannelDistributions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChannelInfos_DistributionsCount",
                table: "ChannelInfos",
                column: "DistributionsCount");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelDistributions_ChannelId",
                table: "ChannelDistributions",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelDistributions_Count",
                table: "ChannelDistributions",
                column: "Count");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelDistributions_FileMetricId",
                table: "ChannelDistributions",
                column: "FileMetricId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelDistributions_FilterId",
                table: "ChannelDistributions",
                column: "FilterId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelDistributions_Id",
                table: "ChannelDistributions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelDistributionValues_Count",
                table: "ChannelDistributionValues",
                column: "Count");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelDistributionValues_DistributionId",
                table: "ChannelDistributionValues",
                column: "DistributionId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelDistributionValues_Id",
                table: "ChannelDistributionValues",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelDistributionValues_Value",
                table: "ChannelDistributionValues",
                column: "Value");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChannelDistributionValues");

            migrationBuilder.DropTable(
                name: "ChannelDistributions");

            migrationBuilder.DropIndex(
                name: "IX_ChannelInfos_DistributionsCount",
                table: "ChannelInfos");

            migrationBuilder.DropColumn(
                name: "DistributionsCount",
                table: "ChannelInfos");
        }
    }
}
