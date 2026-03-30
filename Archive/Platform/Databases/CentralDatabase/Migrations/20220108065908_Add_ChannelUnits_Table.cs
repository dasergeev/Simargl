using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_ChannelUnits_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChannelUnits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelUnits", x => x.Id);
                    table.UniqueConstraint("AK_ChannelUnits_Name", x => x.Name);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChannelUnits_Id",
                table: "ChannelUnits",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelUnits_Name",
                table: "ChannelUnits",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChannelUnits");
        }
    }
}
