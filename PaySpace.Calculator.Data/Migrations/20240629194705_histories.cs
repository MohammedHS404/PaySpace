using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaySpace.Calculator.Data.Migrations
{
    /// <inheritdoc />
    public partial class histories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PostalCode",
                table: "PostalCode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CalculatorSetting",
                table: "CalculatorSetting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CalculatorHistory",
                table: "CalculatorHistory");

            migrationBuilder.RenameTable(
                name: "PostalCode",
                newName: "PostalCodes");

            migrationBuilder.RenameTable(
                name: "CalculatorSetting",
                newName: "CalculatorSettings");

            migrationBuilder.RenameTable(
                name: "CalculatorHistory",
                newName: "CalculatorHistories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostalCodes",
                table: "PostalCodes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalculatorSettings",
                table: "CalculatorSettings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalculatorHistories",
                table: "CalculatorHistories",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PostalCodes",
                table: "PostalCodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CalculatorSettings",
                table: "CalculatorSettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CalculatorHistories",
                table: "CalculatorHistories");

            migrationBuilder.RenameTable(
                name: "PostalCodes",
                newName: "PostalCode");

            migrationBuilder.RenameTable(
                name: "CalculatorSettings",
                newName: "CalculatorSetting");

            migrationBuilder.RenameTable(
                name: "CalculatorHistories",
                newName: "CalculatorHistory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostalCode",
                table: "PostalCode",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalculatorSetting",
                table: "CalculatorSetting",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalculatorHistory",
                table: "CalculatorHistory",
                column: "Id");
        }
    }
}
