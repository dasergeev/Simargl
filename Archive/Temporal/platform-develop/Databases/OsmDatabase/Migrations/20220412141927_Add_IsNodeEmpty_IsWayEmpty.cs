using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    [CLSCompliant(false)]
    public partial class Add_IsNodeEmpty_IsWayEmpty : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsEmpty",
                table: "OsmFiles",
                newName: "IsWayEmpty");

            migrationBuilder.RenameIndex(
                name: "IX_OsmFiles_IsEmpty",
                table: "OsmFiles",
                newName: "IX_OsmFiles_IsWayEmpty");

            migrationBuilder.AddColumn<bool>(
                name: "IsNodeEmpty",
                table: "OsmFiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_OsmFiles_IsNodeEmpty",
                table: "OsmFiles",
                column: "IsNodeEmpty");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OsmFiles_IsNodeEmpty",
                table: "OsmFiles");

            migrationBuilder.DropColumn(
                name: "IsNodeEmpty",
                table: "OsmFiles");

            migrationBuilder.RenameColumn(
                name: "IsWayEmpty",
                table: "OsmFiles",
                newName: "IsEmpty");

            migrationBuilder.RenameIndex(
                name: "IX_OsmFiles_IsWayEmpty",
                table: "OsmFiles",
                newName: "IX_OsmFiles_IsEmpty");
        }
    }
}
