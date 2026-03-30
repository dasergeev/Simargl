using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_Table_OsmNodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OsmNodes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    OsmFileId = table.Column<long>(type: "bigint", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OsmNodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OsmNodes_OsmFile",
                        column: x => x.OsmFileId,
                        principalTable: "OsmFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OsmNodes_Id",
                table: "OsmNodes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OsmNodes_Latitude",
                table: "OsmNodes",
                column: "Latitude");

            migrationBuilder.CreateIndex(
                name: "IX_OsmNodes_Longitude",
                table: "OsmNodes",
                column: "Longitude");

            migrationBuilder.CreateIndex(
                name: "IX_OsmNodes_OsmFileId",
                table: "OsmNodes",
                column: "OsmFileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OsmNodes");
        }
    }
}
