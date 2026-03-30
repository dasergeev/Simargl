using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    public partial class ChangeTableNameOsmTagToOsmNodeTag : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        [CLSCompliant(false)]
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OsmTags");

            migrationBuilder.RenameColumn(
                name: "IsTagsLoad",
                table: "OsmFiles",
                newName: "IsNodeTagsLoad");

            migrationBuilder.RenameIndex(
                name: "IX_OsmFiles_IsTagsLoad",
                table: "OsmFiles",
                newName: "IX_OsmFiles_IsNodeTagsLoad");

            migrationBuilder.CreateTable(
                name: "OsmNodeTags",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OsmNodeId = table.Column<long>(type: "bigint", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OsmNodeTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OsmNodeTags_OsmNode",
                        column: x => x.OsmNodeId,
                        principalTable: "OsmNodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OsmNodeTags_Id",
                table: "OsmNodeTags",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OsmNodeTags_Key",
                table: "OsmNodeTags",
                column: "Key");

            migrationBuilder.CreateIndex(
                name: "IX_OsmNodeTags_OsmNodeId",
                table: "OsmNodeTags",
                column: "OsmNodeId");

            migrationBuilder.CreateIndex(
                name: "IX_OsmNodeTags_Value",
                table: "OsmNodeTags",
                column: "Value");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        [CLSCompliant(false)]
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OsmNodeTags");

            migrationBuilder.RenameColumn(
                name: "IsNodeTagsLoad",
                table: "OsmFiles",
                newName: "IsTagsLoad");

            migrationBuilder.RenameIndex(
                name: "IX_OsmFiles_IsNodeTagsLoad",
                table: "OsmFiles",
                newName: "IX_OsmFiles_IsTagsLoad");

            migrationBuilder.CreateTable(
                name: "OsmTags",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OsmNodeId = table.Column<long>(type: "bigint", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OsmTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OsmTags_OsmNode",
                        column: x => x.OsmNodeId,
                        principalTable: "OsmNodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OsmTags_Id",
                table: "OsmTags",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OsmTags_Key",
                table: "OsmTags",
                column: "Key");

            migrationBuilder.CreateIndex(
                name: "IX_OsmTags_OsmNodeId",
                table: "OsmTags",
                column: "OsmNodeId");

            migrationBuilder.CreateIndex(
                name: "IX_OsmTags_Value",
                table: "OsmTags",
                column: "Value");
        }
    }
}
