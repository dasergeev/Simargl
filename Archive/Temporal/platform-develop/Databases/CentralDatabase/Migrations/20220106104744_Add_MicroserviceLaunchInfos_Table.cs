using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_MicroserviceLaunchInfos_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MicroserviceLaunchInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    MicroserviceId = table.Column<long>(type: "bigint", nullable: false),
                    ComputerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MicroserviceLaunchInfos", x => x.Id);
                    table.UniqueConstraint("AK_MicroserviceLaunchInfos_MicroserviceId_ComputerId", x => new { x.MicroserviceId, x.ComputerId });
                    table.ForeignKey(
                        name: "FK_MicroserviceLaunchInfos_HostComputerInfos",
                        column: x => x.ComputerId,
                        principalTable: "HostComputerInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MicroserviceLaunchInfos_MicroserviceInfos",
                        column: x => x.MicroserviceId,
                        principalTable: "MicroserviceInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MicroserviceLaunchInfos_ComputerId",
                table: "MicroserviceLaunchInfos",
                column: "ComputerId");

            migrationBuilder.CreateIndex(
                name: "IX_MicroserviceLaunchInfos_Id",
                table: "MicroserviceLaunchInfos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MicroserviceLaunchInfos_IsEnable",
                table: "MicroserviceLaunchInfos",
                column: "IsEnable");

            migrationBuilder.CreateIndex(
                name: "IX_MicroserviceLaunchInfos_MicroserviceId",
                table: "MicroserviceLaunchInfos",
                column: "MicroserviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MicroserviceLaunchInfos");
        }
    }
}
