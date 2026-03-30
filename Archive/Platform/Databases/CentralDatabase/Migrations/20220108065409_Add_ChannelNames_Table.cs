using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_ChannelNames_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChannelNames",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelNames", x => x.Id);
                    table.UniqueConstraint("AK_ChannelNames_Name", x => x.Name);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChannelNames_Id",
                table: "ChannelNames",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelNames_Name",
                table: "ChannelNames",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChannelNames");
        }
    }
}
