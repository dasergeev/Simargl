using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_MicroserviceInt32Settings_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MicroserviceInt32Settings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", nullable: false),
                    MicroserviceId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MicroserviceInt32Settings", x => x.Id);
                    table.UniqueConstraint("AK_MicroserviceInt32Settings_MicroserviceId_Name", x => new { x.MicroserviceId, x.Name });
                    table.ForeignKey(
                        name: "FK_MicroserviceInt32Settings_MicroserviceInfos",
                        column: x => x.MicroserviceId,
                        principalTable: "MicroserviceInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MicroserviceInt32Settings_Id",
                table: "MicroserviceInt32Settings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MicroserviceInt32Settings_MicroserviceId",
                table: "MicroserviceInt32Settings",
                column: "MicroserviceId");

            migrationBuilder.CreateIndex(
                name: "IX_MicroserviceInt32Settings_Name",
                table: "MicroserviceInt32Settings",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MicroserviceInt32Settings");
        }
    }
}
