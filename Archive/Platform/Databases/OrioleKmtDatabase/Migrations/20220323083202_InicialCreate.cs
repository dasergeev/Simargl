using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    public partial class InicialCreate : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        [CLSCompliant(false)]
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RawFrames",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawFrames", x => x.Id);
                    table.UniqueConstraint("AK_RawFrames_FilePath", x => x.FilePath);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RawFrames_FilePath",
                table: "RawFrames",
                column: "FilePath");

            migrationBuilder.CreateIndex(
                name: "IX_RawFrames_Id",
                table: "RawFrames",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RawFrames_Time",
                table: "RawFrames",
                column: "Time");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        [CLSCompliant(false)]
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RawFrames");
        }
    }
}
