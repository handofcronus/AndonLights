using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AndonLights.Migrations
{
    /// <inheritdoc />
    public partial class cascade2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyStateStats_States_StateID",
                table: "DailyStateStats");

            migrationBuilder.DropForeignKey(
                name: "FK_MonthlyStateStats_States_StateID",
                table: "MonthlyStateStats");

            migrationBuilder.DropForeignKey(
                name: "FK_States_AndonLights_LightID",
                table: "States");

            migrationBuilder.RenameColumn(
                name: "StateID",
                table: "MonthlyStateStats",
                newName: "StateId");

            migrationBuilder.RenameIndex(
                name: "IX_MonthlyStateStats_StateID",
                table: "MonthlyStateStats",
                newName: "IX_MonthlyStateStats_StateId");

            migrationBuilder.RenameColumn(
                name: "StateID",
                table: "DailyStateStats",
                newName: "StateId");

            migrationBuilder.RenameIndex(
                name: "IX_DailyStateStats_StateID",
                table: "DailyStateStats",
                newName: "IX_DailyStateStats_StateId");

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "MonthlyStateStats",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "DailyStateStats",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DailyStateStats_States_StateId",
                table: "DailyStateStats",
                column: "StateId",
                principalTable: "States",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MonthlyStateStats_States_StateId",
                table: "MonthlyStateStats",
                column: "StateId",
                principalTable: "States",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_States_AndonLights_LightID",
                table: "States",
                column: "LightID",
                principalTable: "AndonLights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyStateStats_States_StateId",
                table: "DailyStateStats");

            migrationBuilder.DropForeignKey(
                name: "FK_MonthlyStateStats_States_StateId",
                table: "MonthlyStateStats");

            migrationBuilder.DropForeignKey(
                name: "FK_States_AndonLights_LightID",
                table: "States");

            migrationBuilder.RenameColumn(
                name: "StateId",
                table: "MonthlyStateStats",
                newName: "StateID");

            migrationBuilder.RenameIndex(
                name: "IX_MonthlyStateStats_StateId",
                table: "MonthlyStateStats",
                newName: "IX_MonthlyStateStats_StateID");

            migrationBuilder.RenameColumn(
                name: "StateId",
                table: "DailyStateStats",
                newName: "StateID");

            migrationBuilder.RenameIndex(
                name: "IX_DailyStateStats_StateId",
                table: "DailyStateStats",
                newName: "IX_DailyStateStats_StateID");

            migrationBuilder.AlterColumn<int>(
                name: "StateID",
                table: "MonthlyStateStats",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "StateID",
                table: "DailyStateStats",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyStateStats_States_StateID",
                table: "DailyStateStats",
                column: "StateID",
                principalTable: "States",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_MonthlyStateStats_States_StateID",
                table: "MonthlyStateStats",
                column: "StateID",
                principalTable: "States",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_States_AndonLights_LightID",
                table: "States",
                column: "LightID",
                principalTable: "AndonLights",
                principalColumn: "Id");
        }
    }
}
