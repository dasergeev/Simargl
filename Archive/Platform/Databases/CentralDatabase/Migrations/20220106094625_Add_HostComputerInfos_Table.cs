using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_HostComputerInfos_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HostComputerInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostComputerInfos", x => x.Id);
                    table.UniqueConstraint("AK_HostComputerInfos_Name", x => x.Name);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HostComputerInfos_Id",
                table: "HostComputerInfos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_HostComputerInfos_Name",
                table: "HostComputerInfos",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HostComputerInfos");
        }
    }
}
