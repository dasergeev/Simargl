using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    [CLSCompliant(false)]
    public partial class Add_OsmWayNodes_Table : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OsmWayNodes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OsmWayId = table.Column<long>(type: "bigint", nullable: false),
                    OsmNodeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OsmWayNodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OsmWayNode_OsmNode",
                        column: x => x.OsmNodeId,
                        principalTable: "OsmNodes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OsmWayNode_OsmWay",
                        column: x => x.OsmWayId,
                        principalTable: "OsmWays",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OsmWayNodes_Id",
                table: "OsmWayNodes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OsmWayNodes_OsmNodeId",
                table: "OsmWayNodes",
                column: "OsmNodeId");

            migrationBuilder.CreateIndex(
                name: "IX_OsmWayNodes_OsmWayId",
                table: "OsmWayNodes",
                column: "OsmWayId");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OsmWayNodes");
        }
    }
}
