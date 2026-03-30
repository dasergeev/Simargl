using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Apeiron.Platform.Databases.Migrations
{
    internal partial class Add_Target_Fields_In_Statistics_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Average",
                table: "Statistics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AverageModulo",
                table: "Statistics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Statistics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Deviation",
                table: "Statistics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DeviationModulo",
                table: "Statistics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Max",
                table: "Statistics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MaxModulo",
                table: "Statistics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Min",
                table: "Statistics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MinModulo",
                table: "Statistics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SquaresSum",
                table: "Statistics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Sum",
                table: "Statistics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SumModulo",
                table: "Statistics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_Average",
                table: "Statistics",
                column: "Average");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_AverageModulo",
                table: "Statistics",
                column: "AverageModulo");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_Count",
                table: "Statistics",
                column: "Count");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_Deviation",
                table: "Statistics",
                column: "Deviation");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_DeviationModulo",
                table: "Statistics",
                column: "DeviationModulo");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_Max",
                table: "Statistics",
                column: "Max");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_MaxModulo",
                table: "Statistics",
                column: "MaxModulo");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_Min",
                table: "Statistics",
                column: "Min");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_MinModulo",
                table: "Statistics",
                column: "MinModulo");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_SquaresSum",
                table: "Statistics",
                column: "SquaresSum");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_Sum",
                table: "Statistics",
                column: "Sum");

            migrationBuilder.CreateIndex(
                name: "IX_Statistics_SumModulo",
                table: "Statistics",
                column: "SumModulo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Statistics_Average",
                table: "Statistics");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_AverageModulo",
                table: "Statistics");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_Count",
                table: "Statistics");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_Deviation",
                table: "Statistics");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_DeviationModulo",
                table: "Statistics");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_Max",
                table: "Statistics");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_MaxModulo",
                table: "Statistics");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_Min",
                table: "Statistics");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_MinModulo",
                table: "Statistics");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_SquaresSum",
                table: "Statistics");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_Sum",
                table: "Statistics");

            migrationBuilder.DropIndex(
                name: "IX_Statistics_SumModulo",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "Average",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "AverageModulo",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "Deviation",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "DeviationModulo",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "Max",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "MaxModulo",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "Min",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "MinModulo",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "SquaresSum",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "Sum",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "SumModulo",
                table: "Statistics");
        }
    }
}
