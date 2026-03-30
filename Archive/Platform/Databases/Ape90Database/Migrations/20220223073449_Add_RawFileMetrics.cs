using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_RawFileMetrics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "RawPowers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsMetrics",
                table: "RawPowers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastAccessTime",
                table: "RawPowers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastWriteTime",
                table: "RawPowers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NameTime",
                table: "RawPowers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "Size",
                table: "RawPowers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "RawGeolocations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsMetrics",
                table: "RawGeolocations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastAccessTime",
                table: "RawGeolocations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastWriteTime",
                table: "RawGeolocations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NameTime",
                table: "RawGeolocations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "Size",
                table: "RawGeolocations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "RawFrames",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsMetrics",
                table: "RawFrames",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastAccessTime",
                table: "RawFrames",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastWriteTime",
                table: "RawFrames",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NameTime",
                table: "RawFrames",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "Size",
                table: "RawFrames",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "RawPowers");

            migrationBuilder.DropColumn(
                name: "IsMetrics",
                table: "RawPowers");

            migrationBuilder.DropColumn(
                name: "LastAccessTime",
                table: "RawPowers");

            migrationBuilder.DropColumn(
                name: "LastWriteTime",
                table: "RawPowers");

            migrationBuilder.DropColumn(
                name: "NameTime",
                table: "RawPowers");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "RawPowers");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "RawGeolocations");

            migrationBuilder.DropColumn(
                name: "IsMetrics",
                table: "RawGeolocations");

            migrationBuilder.DropColumn(
                name: "LastAccessTime",
                table: "RawGeolocations");

            migrationBuilder.DropColumn(
                name: "LastWriteTime",
                table: "RawGeolocations");

            migrationBuilder.DropColumn(
                name: "NameTime",
                table: "RawGeolocations");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "RawGeolocations");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "RawFrames");

            migrationBuilder.DropColumn(
                name: "IsMetrics",
                table: "RawFrames");

            migrationBuilder.DropColumn(
                name: "LastAccessTime",
                table: "RawFrames");

            migrationBuilder.DropColumn(
                name: "LastWriteTime",
                table: "RawFrames");

            migrationBuilder.DropColumn(
                name: "NameTime",
                table: "RawFrames");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "RawFrames");
        }
    }
}
