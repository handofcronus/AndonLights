using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AndonLights.Migrations
{
    /// <inheritdoc />
    public partial class ALC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_States_StateID",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_States_LightID",
                table: "States");

            migrationBuilder.RenameColumn(
                name: "StateID",
                table: "Sessions",
                newName: "StateId");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_StateID",
                table: "Sessions",
                newName: "IX_Sessions_StateId");

            migrationBuilder.AddColumn<int>(
                name: "StateColour",
                table: "States",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "Sessions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_States_LightID",
                table: "States",
                column: "LightID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_States_StateId",
                table: "Sessions",
                column: "StateId",
                principalTable: "States",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_States_StateId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_States_LightID",
                table: "States");

            migrationBuilder.DropColumn(
                name: "StateColour",
                table: "States");

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
                name: "StateId",
                table: "Sessions",
                newName: "StateID");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_StateId",
                table: "Sessions",
                newName: "IX_Sessions_StateID");

            migrationBuilder.AlterColumn<int>(
                name: "StateID",
                table: "Sessions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_States_LightID",
                table: "States",
                column: "LightID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_States_StateID",
                table: "Sessions",
                column: "StateID",
                principalTable: "States",
                principalColumn: "ID");
        }
    }
}
