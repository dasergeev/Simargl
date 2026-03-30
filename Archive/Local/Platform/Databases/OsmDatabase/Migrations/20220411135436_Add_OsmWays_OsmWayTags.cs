using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    [CLSCompliant(false)]
    public partial class Add_OsmWays_OsmWayTags : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsWayTagsLoad",
                table: "OsmFiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsWaysLoad",
                table: "OsmFiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "OsmWays",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    OsmFileId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OsmWays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OsmWays_OsmFile",
                        column: x => x.OsmFileId,
                        principalTable: "OsmFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OsmWayTags",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OsmWayId = table.Column<long>(type: "bigint", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OsmWayTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OsmWayTags_OsmWay",
                        column: x => x.OsmWayId,
                        principalTable: "OsmWays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OsmFiles_IsWaysLoad",
                table: "OsmFiles",
                column: "IsWaysLoad");

            migrationBuilder.CreateIndex(
                name: "IX_OsmFiles_IsWayTagsLoad",
                table: "OsmFiles",
                column: "IsWayTagsLoad");

            migrationBuilder.CreateIndex(
                name: "IX_OsmWays_Id",
                table: "OsmWays",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OsmWays_OsmFileId",
                table: "OsmWays",
                column: "OsmFileId");

            migrationBuilder.CreateIndex(
                name: "IX_OsmWayTags_Id",
                table: "OsmWayTags",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OsmWayTags_Key",
                table: "OsmWayTags",
                column: "Key");

            migrationBuilder.CreateIndex(
                name: "IX_OsmWayTags_OsmWayId",
                table: "OsmWayTags",
                column: "OsmWayId");

            migrationBuilder.CreateIndex(
                name: "IX_OsmWayTags_Value",
                table: "OsmWayTags",
                column: "Value");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OsmWayTags");

            migrationBuilder.DropTable(
                name: "OsmWays");

            migrationBuilder.DropIndex(
                name: "IX_OsmFiles_IsWaysLoad",
                table: "OsmFiles");

            migrationBuilder.DropIndex(
                name: "IX_OsmFiles_IsWayTagsLoad",
                table: "OsmFiles");

            migrationBuilder.DropColumn(
                name: "IsWayTagsLoad",
                table: "OsmFiles");

            migrationBuilder.DropColumn(
                name: "IsWaysLoad",
                table: "OsmFiles");
        }
    }
}
