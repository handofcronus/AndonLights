using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AndonLights.Migrations
{
    /// <inheritdoc />
    public partial class postgresql : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AndonLights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CurrentState = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AndonLights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LightID = table.Column<int>(type: "integer", nullable: false),
                    StateColour = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.ID);
                    table.ForeignKey(
                        name: "FK_States_AndonLights_LightID",
                        column: x => x.LightID,
                        principalTable: "AndonLights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyStateStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StateId = table.Column<int>(type: "integer", nullable: false),
                    NumberOfEntries = table.Column<int>(type: "integer", nullable: false),
                    MinutesSpentInState = table.Column<double>(type: "double precision", nullable: false),
                    DateOfStats = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyStateStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyStateStats_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonthlyStateStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StateId = table.Column<int>(type: "integer", nullable: false),
                    NumberOfEntries = table.Column<int>(type: "integer", nullable: false),
                    MinutesSpentInState = table.Column<double>(type: "double precision", nullable: false),
                    DateOfStats = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyStateStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthlyStateStats_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StateId = table.Column<int>(type: "integer", nullable: false),
                    LenghtOfSessionInMinutes = table.Column<double>(type: "double precision", nullable: false),
                    ErrorMessage = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    InTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OutTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyStateStats_StateId",
                table: "DailyStateStats",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyStateStats_StateId",
                table: "MonthlyStateStats",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_StateId",
                table: "Sessions",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_States_LightID",
                table: "States",
                column: "LightID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyStateStats");

            migrationBuilder.DropTable(
                name: "MonthlyStateStats");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "AndonLights");
        }
    }
}
