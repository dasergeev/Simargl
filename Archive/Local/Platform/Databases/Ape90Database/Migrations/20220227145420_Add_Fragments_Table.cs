using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_Fragments_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fragments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChannelInfoId = table.Column<long>(type: "bigint", nullable: false),
                    FrameInfoId = table.Column<long>(type: "bigint", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDataValid = table.Column<bool>(type: "bit", nullable: false),
                    SpeedZero = table.Column<double>(type: "float", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Min = table.Column<double>(type: "float", nullable: false),
                    Max = table.Column<double>(type: "float", nullable: false),
                    Average = table.Column<double>(type: "float", nullable: false),
                    Deviation = table.Column<double>(type: "float", nullable: false),
                    Sum = table.Column<double>(type: "float", nullable: false),
                    IsGpsValid = table.Column<bool>(type: "bit", nullable: false),
                    Speed = table.Column<double>(type: "float", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Altitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fragments", x => x.Id);
                    table.UniqueConstraint("AK_Fragments_ChannelInfoId_FrameInfoId_Index", x => new { x.ChannelInfoId, x.FrameInfoId, x.Index });
                    table.ForeignKey(
                        name: "FK_Fragments_ChannelInfos",
                        column: x => x.ChannelInfoId,
                        principalTable: "ChannelInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fragments_FrameInfos",
                        column: x => x.FrameInfoId,
                        principalTable: "FrameInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_Altitude",
                table: "Fragments",
                column: "Altitude");

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_Average",
                table: "Fragments",
                column: "Average");

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_Deviation",
                table: "Fragments",
                column: "Deviation");

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_FrameInfoId",
                table: "Fragments",
                column: "FrameInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_IsDataValid",
                table: "Fragments",
                column: "IsDataValid");

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_IsGpsValid",
                table: "Fragments",
                column: "IsGpsValid");

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_Latitude",
                table: "Fragments",
                column: "Latitude");

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_Longitude",
                table: "Fragments",
                column: "Longitude");

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_Max",
                table: "Fragments",
                column: "Max");

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_Min",
                table: "Fragments",
                column: "Min");

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_Speed",
                table: "Fragments",
                column: "Speed");

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_SpeedZero",
                table: "Fragments",
                column: "SpeedZero");

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_Sum",
                table: "Fragments",
                column: "Sum");

            migrationBuilder.CreateIndex(
                name: "IX_Fragments_Time",
                table: "Fragments",
                column: "Time");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fragments");
        }
    }
}
