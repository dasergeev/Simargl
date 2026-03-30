using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    [CLSCompliant(false)]
    public partial class AnalyzedFrame_Add_Fields_for_Processed : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CurvatureAverage",
                table: "AnalyzedFrames",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CurvatureMax",
                table: "AnalyzedFrames",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CurvatureMin",
                table: "AnalyzedFrames",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DurationBraking",
                table: "AnalyzedFrames",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DurationParking",
                table: "AnalyzedFrames",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DurationRunout",
                table: "AnalyzedFrames",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DurationTraction",
                table: "AnalyzedFrames",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPocessed",
                table: "AnalyzedFrames",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "AnalyzedFrames",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "AnalyzedFrames",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "TractionEffortCount",
                table: "AnalyzedFrames",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "TractionEffortMax",
                table: "AnalyzedFrames",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TractionEffortMin",
                table: "AnalyzedFrames",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TractionEffortSquaredSum",
                table: "AnalyzedFrames",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TractionEffortSum",
                table: "AnalyzedFrames",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_AnalyzedFrames_IsPocessed",
                table: "AnalyzedFrames",
                column: "IsPocessed");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AnalyzedFrames_IsPocessed",
                table: "AnalyzedFrames");

            migrationBuilder.DropColumn(
                name: "CurvatureAverage",
                table: "AnalyzedFrames");

            migrationBuilder.DropColumn(
                name: "CurvatureMax",
                table: "AnalyzedFrames");

            migrationBuilder.DropColumn(
                name: "CurvatureMin",
                table: "AnalyzedFrames");

            migrationBuilder.DropColumn(
                name: "DurationBraking",
                table: "AnalyzedFrames");

            migrationBuilder.DropColumn(
                name: "DurationParking",
                table: "AnalyzedFrames");

            migrationBuilder.DropColumn(
                name: "DurationRunout",
                table: "AnalyzedFrames");

            migrationBuilder.DropColumn(
                name: "DurationTraction",
                table: "AnalyzedFrames");

            migrationBuilder.DropColumn(
                name: "IsPocessed",
                table: "AnalyzedFrames");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "AnalyzedFrames");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "AnalyzedFrames");

            migrationBuilder.DropColumn(
                name: "TractionEffortCount",
                table: "AnalyzedFrames");

            migrationBuilder.DropColumn(
                name: "TractionEffortMax",
                table: "AnalyzedFrames");

            migrationBuilder.DropColumn(
                name: "TractionEffortMin",
                table: "AnalyzedFrames");

            migrationBuilder.DropColumn(
                name: "TractionEffortSquaredSum",
                table: "AnalyzedFrames");

            migrationBuilder.DropColumn(
                name: "TractionEffortSum",
                table: "AnalyzedFrames");
        }
    }
}
