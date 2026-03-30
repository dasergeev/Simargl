using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_Common_Fields_In_IdentityMessages_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "IdentityMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "GlobalIdentifierId",
                table: "IdentityMessages",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PacketIdentifier",
                table: "IdentityMessages",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Port",
                table: "IdentityMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte>(
                name: "Source",
                table: "IdentityMessages",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "IdentityMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "IdentityMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_IdentityMessages_GlobalIdentifierId",
                table: "IdentityMessages",
                column: "GlobalIdentifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityMessages_GlobalIdentifiers_GlobalIdentifierId",
                table: "IdentityMessages",
                column: "GlobalIdentifierId",
                principalTable: "GlobalIdentifiers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdentityMessages_GlobalIdentifiers_GlobalIdentifierId",
                table: "IdentityMessages");

            migrationBuilder.DropIndex(
                name: "IX_IdentityMessages_GlobalIdentifierId",
                table: "IdentityMessages");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "IdentityMessages");

            migrationBuilder.DropColumn(
                name: "GlobalIdentifierId",
                table: "IdentityMessages");

            migrationBuilder.DropColumn(
                name: "PacketIdentifier",
                table: "IdentityMessages");

            migrationBuilder.DropColumn(
                name: "Port",
                table: "IdentityMessages");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "IdentityMessages");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "IdentityMessages");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "IdentityMessages");
        }
    }
}
