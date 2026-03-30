using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_ChannelsCount_Fiels_In_FrameInfos_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChannelsCount",
                table: "FrameInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FrameInfos_ChannelsCount",
                table: "FrameInfos",
                column: "ChannelsCount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FrameInfos_ChannelsCount",
                table: "FrameInfos");

            migrationBuilder.DropColumn(
                name: "ChannelsCount",
                table: "FrameInfos");
        }
    }
}
