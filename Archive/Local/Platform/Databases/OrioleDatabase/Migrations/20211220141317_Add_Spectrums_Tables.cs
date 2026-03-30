using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_Spectrums_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAnalyzed",
                table: "Frames",
                newName: "IsSpectrum");

            migrationBuilder.RenameIndex(
                name: "IX_Frames_IsAnalyzed",
                table: "Frames",
                newName: "IX_Frames_IsSpectrum");

            migrationBuilder.CreateTable(
                name: "Spectrums",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChannelId = table.Column<int>(type: "int", nullable: false),
                    RegistrarId = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spectrums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spectrums_Channels",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Spectrums_Frames",
                        columns: x => new { x.RegistrarId, x.Timestamp },
                        principalTable: "Frames",
                        principalColumns: new[] { "RegistrarId", "Timestamp" });
                    table.ForeignKey(
                        name: "FK_Spectrums_Registrars",
                        column: x => x.RegistrarId,
                        principalTable: "Registrars",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Amplitudes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpectrumId = table.Column<long>(type: "bigint", nullable: false),
                    Frequency = table.Column<double>(type: "float", nullable: false),
                    Real = table.Column<double>(type: "float", nullable: false),
                    Imaginary = table.Column<double>(type: "float", nullable: false),
                    Magnitude = table.Column<double>(type: "float", nullable: false),
                    Phase = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amplitudes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Amplitudes_Spectrums",
                        column: x => x.SpectrumId,
                        principalTable: "Spectrums",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amplitudes_Frequency",
                table: "Amplitudes",
                column: "Frequency");

            migrationBuilder.CreateIndex(
                name: "IX_Amplitudes_Magnitude",
                table: "Amplitudes",
                column: "Magnitude");

            migrationBuilder.CreateIndex(
                name: "IX_Amplitudes_SpectrumId",
                table: "Amplitudes",
                column: "SpectrumId");

            migrationBuilder.CreateIndex(
                name: "IX_Spectrums_ChannelId",
                table: "Spectrums",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Spectrums_RegistrarId_Timestamp",
                table: "Spectrums",
                columns: new[] { "RegistrarId", "Timestamp" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amplitudes");

            migrationBuilder.DropTable(
                name: "Spectrums");

            migrationBuilder.RenameColumn(
                name: "IsSpectrum",
                table: "Frames",
                newName: "IsAnalyzed");

            migrationBuilder.RenameIndex(
                name: "IX_Frames_IsSpectrum",
                table: "Frames",
                newName: "IX_Frames_IsAnalyzed");
        }
    }
}
