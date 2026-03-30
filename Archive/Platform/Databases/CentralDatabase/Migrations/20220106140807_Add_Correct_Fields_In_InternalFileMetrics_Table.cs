using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_Correct_Fields_In_InternalFileMetrics_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "BytesSum",
                table: "InternalFileMetrics",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BytesSum",
                table: "InternalFileMetrics",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
