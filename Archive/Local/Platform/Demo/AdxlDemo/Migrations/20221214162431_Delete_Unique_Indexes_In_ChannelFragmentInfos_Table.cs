using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Demo.Migrations
{
    internal partial class Delete_Unique_Indexes_In_ChannelFragmentInfos_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChannelFragmentInfos_BeginTime",
                table: "ChannelFragmentInfos");

            migrationBuilder.DropIndex(
                name: "IX_ChannelFragmentInfos_EndTime",
                table: "ChannelFragmentInfos");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelFragmentInfos_BeginTime",
                table: "ChannelFragmentInfos",
                column: "BeginTime");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelFragmentInfos_EndTime",
                table: "ChannelFragmentInfos",
                column: "EndTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChannelFragmentInfos_BeginTime",
                table: "ChannelFragmentInfos");

            migrationBuilder.DropIndex(
                name: "IX_ChannelFragmentInfos_EndTime",
                table: "ChannelFragmentInfos");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelFragmentInfos_BeginTime",
                table: "ChannelFragmentInfos",
                column: "BeginTime",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChannelFragmentInfos_EndTime",
                table: "ChannelFragmentInfos",
                column: "EndTime",
                unique: true);
        }
    }
}
