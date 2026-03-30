using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_AlternateKey_In_ChannelStatistics_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_ChannelStatistics_ChannelId_FilterId_FileMetricId",
                table: "ChannelStatistics",
                columns: new[] { "ChannelId", "FilterId", "FileMetricId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ChannelStatistics_ChannelId_FilterId_FileMetricId",
                table: "ChannelStatistics");
        }
    }
}
