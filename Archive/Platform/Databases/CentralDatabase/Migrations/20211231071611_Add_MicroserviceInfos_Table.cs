using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_MicroserviceInfos_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MicroserviceInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NextStepDelay = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MicroserviceInfos", x => x.Id);
                    table.UniqueConstraint("AK_MicroserviceInfos_Name", x => x.Name);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MicroserviceInfos_Id",
                table: "MicroserviceInfos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MicroserviceInfos_Name",
                table: "MicroserviceInfos",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MicroserviceInfos");
        }
    }
}
