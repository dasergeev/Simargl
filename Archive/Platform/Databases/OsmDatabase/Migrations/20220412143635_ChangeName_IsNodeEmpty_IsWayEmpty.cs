using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    [CLSCompliant(false)]
    public partial class ChangeName_IsNodeEmpty_IsWayEmpty : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsWayEmpty",
                table: "OsmFiles",
                newName: "IsWaysEmpty");

            migrationBuilder.RenameColumn(
                name: "IsNodeEmpty",
                table: "OsmFiles",
                newName: "IsNodesEmpty");

            migrationBuilder.RenameIndex(
                name: "IX_OsmFiles_IsWayEmpty",
                table: "OsmFiles",
                newName: "IX_OsmFiles_IsWaysEmpty");

            migrationBuilder.RenameIndex(
                name: "IX_OsmFiles_IsNodeEmpty",
                table: "OsmFiles",
                newName: "IX_OsmFiles_IsNodesEmpty");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsWaysEmpty",
                table: "OsmFiles",
                newName: "IsWayEmpty");

            migrationBuilder.RenameColumn(
                name: "IsNodesEmpty",
                table: "OsmFiles",
                newName: "IsNodeEmpty");

            migrationBuilder.RenameIndex(
                name: "IX_OsmFiles_IsWaysEmpty",
                table: "OsmFiles",
                newName: "IX_OsmFiles_IsWayEmpty");

            migrationBuilder.RenameIndex(
                name: "IX_OsmFiles_IsNodesEmpty",
                table: "OsmFiles",
                newName: "IX_OsmFiles_IsNodeEmpty");
        }
    }
}
