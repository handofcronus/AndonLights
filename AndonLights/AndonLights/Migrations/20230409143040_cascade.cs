using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AndonLights.Migrations
{
    /// <inheritdoc />
    public partial class cascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_States_AndonLights_LightID",
                table: "States");

            migrationBuilder.AddForeignKey(
                name: "FK_States_AndonLights_LightID",
                table: "States",
                column: "LightID",
                principalTable: "AndonLights",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_States_AndonLights_LightID",
                table: "States");

            migrationBuilder.AddForeignKey(
                name: "FK_States_AndonLights_LightID",
                table: "States",
                column: "LightID",
                principalTable: "AndonLights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
