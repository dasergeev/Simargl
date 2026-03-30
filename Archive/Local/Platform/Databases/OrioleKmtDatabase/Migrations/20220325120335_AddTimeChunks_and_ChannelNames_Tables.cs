using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AddTimeChunks_and_ChannelNames_Tables : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        [CLSCompliant(false)]
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeChunks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BeginTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeChunks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChannelNames",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeChunkId = table.Column<long>(type: "bigint", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChannelNames_TimeChunk",
                        column: x => x.TimeChunkId,
                        principalTable: "TimeChunks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChannelNames_Id",
                table: "ChannelNames",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelNames_Index",
                table: "ChannelNames",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelNames_Name",
                table: "ChannelNames",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelNames_TimeChunkId",
                table: "ChannelNames",
                column: "TimeChunkId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeChunks_BeginTime",
                table: "TimeChunks",
                column: "BeginTime");

            migrationBuilder.CreateIndex(
                name: "IX_TimeChunks_EndTime",
                table: "TimeChunks",
                column: "EndTime");

            migrationBuilder.CreateIndex(
                name: "IX_TimeChunks_Id",
                table: "TimeChunks",
                column: "Id");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        [CLSCompliant(false)]
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChannelNames");

            migrationBuilder.DropTable(
                name: "TimeChunks");
        }
    }
}
