using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AndonLights.Migrations
{
    /// <inheritdoc />
    public partial class keyswap2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NewApiKey",
                table: "Clients",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewApiKey",
                table: "Clients");
        }
    }
}
