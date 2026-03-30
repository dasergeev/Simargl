using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Simargl.Projects.Oriole.Oriole01Storage.Migrations
{
    /// <inheritdoc />
    internal partial class Add_ChannelNames_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChannelNames",
                columns: table => new
                {
                    Key = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Serial = table.Column<string>(type: "text", nullable: false),
                    Axis = table.Column<int>(type: "integer", nullable: false),
                    Direct = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelNames", x => x.Key);
                    table.UniqueConstraint("AK_ChannelNames_Serial_Axis", x => new { x.Serial, x.Axis });
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChannelNames_Name",
                table: "ChannelNames",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChannelNames");
        }
    }
}
