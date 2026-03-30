using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_RawGeolocations_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RawGeolocations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RawDirectoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawGeolocations", x => x.Id);
                    table.UniqueConstraint("AK_RawGeolocations_Path", x => x.Path);
                    table.ForeignKey(
                        name: "FK_RawGeolocations_RawDirectories",
                        column: x => x.RawDirectoryId,
                        principalTable: "RawDirectories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RawGeolocations_RawDirectoryId",
                table: "RawGeolocations",
                column: "RawDirectoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RawGeolocations");
        }
    }
}
