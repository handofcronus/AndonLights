using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AndonLights.Migrations
{
    /// <inheritdoc />
    public partial class errorMsg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_States_Sessions_ID",
                table: "States");

            migrationBuilder.DropColumn(
                name: "ErrorMessage",
                table: "Sessions");

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "States",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "LastErrorMessage",
                table: "AndonLights",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastErrorMessage",
                table: "AndonLights");

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "States",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "ErrorMessage",
                table: "Sessions",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_States_Sessions_ID",
                table: "States",
                column: "ID",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
