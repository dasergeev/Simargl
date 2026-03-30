using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_Extra_Fields_In_Geolocations_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Age",
                table: "Geolocations",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Geoidal",
                table: "Geolocations",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Hdop",
                table: "Geolocations",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Knots",
                table: "Geolocations",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MagneticVariation",
                table: "Geolocations",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Mode",
                table: "Geolocations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Satellites",
                table: "Geolocations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Solution",
                table: "Geolocations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Station",
                table: "Geolocations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Valid",
                table: "Geolocations",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Geolocations");

            migrationBuilder.DropColumn(
                name: "Geoidal",
                table: "Geolocations");

            migrationBuilder.DropColumn(
                name: "Hdop",
                table: "Geolocations");

            migrationBuilder.DropColumn(
                name: "Knots",
                table: "Geolocations");

            migrationBuilder.DropColumn(
                name: "MagneticVariation",
                table: "Geolocations");

            migrationBuilder.DropColumn(
                name: "Mode",
                table: "Geolocations");

            migrationBuilder.DropColumn(
                name: "Satellites",
                table: "Geolocations");

            migrationBuilder.DropColumn(
                name: "Solution",
                table: "Geolocations");

            migrationBuilder.DropColumn(
                name: "Station",
                table: "Geolocations");

            migrationBuilder.DropColumn(
                name: "Valid",
                table: "Geolocations");
        }
    }
}
