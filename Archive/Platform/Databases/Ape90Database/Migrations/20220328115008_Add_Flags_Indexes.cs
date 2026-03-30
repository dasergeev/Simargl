using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_Flags_Indexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FrameInfos_IsAnalyzed",
                table: "FrameInfos",
                column: "IsAnalyzed");

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_IsLoadedIntoLongFrame",
                table: "Fragments",
                column: "IsLoadedIntoLongFrame");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FrameInfos_IsAnalyzed",
                table: "FrameInfos");

            migrationBuilder.DropIndex(
                name: "IX_Fragments_IsLoadedIntoLongFrame",
                table: "Fragments");
        }
    }
}
