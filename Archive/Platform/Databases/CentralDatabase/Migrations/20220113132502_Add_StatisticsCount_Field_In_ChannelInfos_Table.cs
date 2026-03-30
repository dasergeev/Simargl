using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_StatisticsCount_Field_In_ChannelInfos_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatisticsCount",
                table: "ChannelInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ChannelInfos_StatisticsCount",
                table: "ChannelInfos",
                column: "StatisticsCount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChannelInfos_StatisticsCount",
                table: "ChannelInfos");

            migrationBuilder.DropColumn(
                name: "StatisticsCount",
                table: "ChannelInfos");
        }
    }
}
