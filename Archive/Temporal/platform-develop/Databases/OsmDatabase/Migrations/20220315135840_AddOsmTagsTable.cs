using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class AddOsmTagsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OsmTags");
        }
    }
}
