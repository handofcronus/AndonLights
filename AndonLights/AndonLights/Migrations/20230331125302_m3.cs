using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AndonLights.Migrations
{
    /// <inheritdoc />
    public partial class m3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_states",
                table: "states");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sessions",
                table: "sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_andonLights",
                table: "andonLights");

            migrationBuilder.RenameTable(
                name: "states",
                newName: "States");

            migrationBuilder.RenameTable(
                name: "sessions",
                newName: "Sessions");

            migrationBuilder.RenameTable(
                name: "andonLights",
                newName: "AndonLights");

            migrationBuilder.AddColumn<int>(
                name: "LightID",
                table: "States",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StateID",
                table: "Sessions",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_States",
                table: "States",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sessions",
                table: "Sessions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AndonLights",
                table: "AndonLights",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DailyStateStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfEntries = table.Column<int>(type: "int", nullable: false),
                    MinutesSpentInState = table.Column<double>(type: "float", nullable: false),
                    DayOfStats = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StateID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyStateStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyStateStats_States_StateID",
                        column: x => x.StateID,
                        principalTable: "States",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "MonthlyStateStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfEntries = table.Column<int>(type: "int", nullable: false),
                    MinutesSpentInState = table.Column<double>(type: "float", nullable: false),
                    MonthOfStats = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StateID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyStateStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthlyStateStats_States_StateID",
                        column: x => x.StateID,
                        principalTable: "States",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_States_LightID",
                table: "States",
                column: "LightID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_StateID",
                table: "Sessions",
                column: "StateID");

            migrationBuilder.CreateIndex(
                name: "IX_DailyStateStats_StateID",
                table: "DailyStateStats",
                column: "StateID");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyStateStats_StateID",
                table: "MonthlyStateStats",
                column: "StateID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_States_StateID",
                table: "Sessions",
                column: "StateID",
                principalTable: "States",
                principalColumn: "ID");

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
                name: "FK_Sessions_States_StateID",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_States_AndonLights_LightID",
                table: "States");

            migrationBuilder.DropTable(
                name: "DailyStateStats");

            migrationBuilder.DropTable(
                name: "MonthlyStateStats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_States",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_States_LightID",
                table: "States");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sessions",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_StateID",
                table: "Sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AndonLights",
                table: "AndonLights");

            migrationBuilder.DropColumn(
                name: "LightID",
                table: "States");

            migrationBuilder.DropColumn(
                name: "StateID",
                table: "Sessions");

            migrationBuilder.RenameTable(
                name: "States",
                newName: "states");

            migrationBuilder.RenameTable(
                name: "Sessions",
                newName: "sessions");

            migrationBuilder.RenameTable(
                name: "AndonLights",
                newName: "andonLights");

            migrationBuilder.AddPrimaryKey(
                name: "PK_states",
                table: "states",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sessions",
                table: "sessions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_andonLights",
                table: "andonLights",
                column: "Id");
        }
    }
}
