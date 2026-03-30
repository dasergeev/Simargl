using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_Changed_And_Speed_Fields_In_DatabaseTables_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ChangedInDay",
                table: "DatabaseTables",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ChangedInHour",
                table: "DatabaseTables",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ChangedInMonth",
                table: "DatabaseTables",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Count",
                table: "DatabaseTables",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<double>(
                name: "SpeedPerDay",
                table: "DatabaseTables",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SpeedPerHour",
                table: "DatabaseTables",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SpeedPerMonth",
                table: "DatabaseTables",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangedInDay",
                table: "DatabaseTables");

            migrationBuilder.DropColumn(
                name: "ChangedInHour",
                table: "DatabaseTables");

            migrationBuilder.DropColumn(
                name: "ChangedInMonth",
                table: "DatabaseTables");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "DatabaseTables");

            migrationBuilder.DropColumn(
                name: "SpeedPerDay",
                table: "DatabaseTables");

            migrationBuilder.DropColumn(
                name: "SpeedPerHour",
                table: "DatabaseTables");

            migrationBuilder.DropColumn(
                name: "SpeedPerMonth",
                table: "DatabaseTables");
        }
    }
}
