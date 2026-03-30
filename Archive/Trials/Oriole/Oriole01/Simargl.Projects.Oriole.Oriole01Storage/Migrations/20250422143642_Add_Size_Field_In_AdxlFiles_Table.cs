using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simargl.Projects.Oriole.Oriole01Storage.Migrations
{
    /// <inheritdoc />
    internal partial class Add_Size_Field_In_AdxlFiles_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Size",
                table: "AdxlFiles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "AdxlFiles");
        }
    }
}
