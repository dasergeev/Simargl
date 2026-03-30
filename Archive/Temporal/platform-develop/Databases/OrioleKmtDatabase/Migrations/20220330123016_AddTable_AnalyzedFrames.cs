using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    [CLSCompliant(false)]
    public partial class AddTable_AnalyzedFrames : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnalyzedFrames",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyzedFrames", x => x.Id);
                    table.UniqueConstraint("AK_AnalyzedFrames_FilePath", x => x.FilePath);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnalyzedFrames_FilePath",
                table: "AnalyzedFrames",
                column: "FilePath");

            migrationBuilder.CreateIndex(
                name: "IX_AnalyzedFrames_Id",
                table: "AnalyzedFrames",
                column: "Id");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalyzedFrames");
        }
    }
}
