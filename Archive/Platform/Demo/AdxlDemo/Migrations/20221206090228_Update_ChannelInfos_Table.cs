using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Demo.Migrations
{
    internal partial class Update_ChannelInfos_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChannelType",
                table: "ChannelInfos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Cutoff",
                table: "ChannelInfos",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Sampling",
                table: "ChannelInfos",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "SerialNumber",
                table: "ChannelInfos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "SignalNumber",
                table: "ChannelInfos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChannelType",
                table: "ChannelInfos");

            migrationBuilder.DropColumn(
                name: "Cutoff",
                table: "ChannelInfos");

            migrationBuilder.DropColumn(
                name: "Sampling",
                table: "ChannelInfos");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "ChannelInfos");

            migrationBuilder.DropColumn(
                name: "SignalNumber",
                table: "ChannelInfos");
        }
    }
}
