using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_Cascade_Delete_In_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amplitudes_Spectrums",
                table: "Amplitudes");

            migrationBuilder.DropForeignKey(
                name: "FK_Spectrums_Frames",
                table: "Spectrums");

            migrationBuilder.AddForeignKey(
                name: "FK_Amplitudes_Spectrums",
                table: "Amplitudes",
                column: "SpectrumId",
                principalTable: "Spectrums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Spectrums_Frames",
                table: "Spectrums",
                columns: new[] { "RegistrarId", "Timestamp" },
                principalTable: "Frames",
                principalColumns: new[] { "RegistrarId", "Timestamp" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amplitudes_Spectrums",
                table: "Amplitudes");

            migrationBuilder.DropForeignKey(
                name: "FK_Spectrums_Frames",
                table: "Spectrums");

            migrationBuilder.AddForeignKey(
                name: "FK_Amplitudes_Spectrums",
                table: "Amplitudes",
                column: "SpectrumId",
                principalTable: "Spectrums",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Spectrums_Frames",
                table: "Spectrums",
                columns: new[] { "RegistrarId", "Timestamp" },
                principalTable: "Frames",
                principalColumns: new[] { "RegistrarId", "Timestamp" });
        }
    }
}
