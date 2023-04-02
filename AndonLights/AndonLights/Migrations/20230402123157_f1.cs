using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AndonLights.Migrations
{
    /// <inheritdoc />
    public partial class f1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GreenStateId",
                table: "AndonLights");

            migrationBuilder.DropColumn(
                name: "RedStateId",
                table: "AndonLights");

            migrationBuilder.DropColumn(
                name: "YellowStateId",
                table: "AndonLights");

            migrationBuilder.RenameColumn(
                name: "MonthOfStats",
                table: "MonthlyStateStats",
                newName: "DateOfStats");

            migrationBuilder.RenameColumn(
                name: "DayOfStats",
                table: "DailyStateStats",
                newName: "DateOfStats");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfStats",
                table: "MonthlyStateStats",
                newName: "MonthOfStats");

            migrationBuilder.RenameColumn(
                name: "DateOfStats",
                table: "DailyStateStats",
                newName: "DayOfStats");

            migrationBuilder.AddColumn<int>(
                name: "GreenStateId",
                table: "AndonLights",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RedStateId",
                table: "AndonLights",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YellowStateId",
                table: "AndonLights",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
