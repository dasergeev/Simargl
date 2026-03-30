using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_Recorders_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recorders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransferIdentificator = table.Column<int>(type: "int", nullable: false),
                    TransferDirectoryId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recorders", x => x.Id);
                    table.UniqueConstraint("AK_Recorders_Name", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Recorders_InternalDirectories",
                        column: x => x.TransferDirectoryId,
                        principalTable: "InternalDirectories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recorders_Id",
                table: "Recorders",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Recorders_Name",
                table: "Recorders",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Recorders_TransferDirectoryId",
                table: "Recorders",
                column: "TransferDirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Recorders_TransferIdentificator",
                table: "Recorders",
                column: "TransferIdentificator");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recorders");
        }
    }
}
