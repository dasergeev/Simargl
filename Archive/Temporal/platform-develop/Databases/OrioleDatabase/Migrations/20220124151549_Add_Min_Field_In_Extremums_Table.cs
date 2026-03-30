using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_Min_Field_In_Extremums_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExtremumIndex",
                table: "Extremums",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "ExtremumValue",
                table: "Extremums",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsMin",
                table: "Extremums",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Extremums",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Uxb1Value",
                table: "Extremums",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Uxb2Value",
                table: "Extremums",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Uxk1Value",
                table: "Extremums",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Uxk2Value",
                table: "Extremums",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "UxrValue",
                table: "Extremums",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Uyb1Value",
                table: "Extremums",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Uyb2Value",
                table: "Extremums",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Uyk1Value",
                table: "Extremums",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Uyk2Value",
                table: "Extremums",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "UyrValue",
                table: "Extremums",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Uzb1Value",
                table: "Extremums",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Uzb2Value",
                table: "Extremums",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Uzk1Value",
                table: "Extremums",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Uzk2Value",
                table: "Extremums",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "UzrValue",
                table: "Extremums",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtremumIndex",
                table: "Extremums");

            migrationBuilder.DropColumn(
                name: "ExtremumValue",
                table: "Extremums");

            migrationBuilder.DropColumn(
                name: "IsMin",
                table: "Extremums");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "Extremums");

            migrationBuilder.DropColumn(
                name: "Uxb1Value",
                table: "Extremums");

            migrationBuilder.DropColumn(
                name: "Uxb2Value",
                table: "Extremums");

            migrationBuilder.DropColumn(
                name: "Uxk1Value",
                table: "Extremums");

            migrationBuilder.DropColumn(
                name: "Uxk2Value",
                table: "Extremums");

            migrationBuilder.DropColumn(
                name: "UxrValue",
                table: "Extremums");

            migrationBuilder.DropColumn(
                name: "Uyb1Value",
                table: "Extremums");

            migrationBuilder.DropColumn(
                name: "Uyb2Value",
                table: "Extremums");

            migrationBuilder.DropColumn(
                name: "Uyk1Value",
                table: "Extremums");

            migrationBuilder.DropColumn(
                name: "Uyk2Value",
                table: "Extremums");

            migrationBuilder.DropColumn(
                name: "UyrValue",
                table: "Extremums");

            migrationBuilder.DropColumn(
                name: "Uzb1Value",
                table: "Extremums");

            migrationBuilder.DropColumn(
                name: "Uzb2Value",
                table: "Extremums");

            migrationBuilder.DropColumn(
                name: "Uzk1Value",
                table: "Extremums");

            migrationBuilder.DropColumn(
                name: "Uzk2Value",
                table: "Extremums");

            migrationBuilder.DropColumn(
                name: "UzrValue",
                table: "Extremums");
        }
    }
}
