using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AndonLights.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 901,
                column: "LenghtOfSessionInMinutes",
                value: 89.0);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 902,
                column: "LenghtOfSessionInMinutes",
                value: 840.0);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 903,
                column: "LenghtOfSessionInMinutes",
                value: 385.0);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 904,
                column: "LenghtOfSessionInMinutes",
                value: 215.0);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 905,
                column: "LenghtOfSessionInMinutes",
                value: 121.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 901,
                column: "LenghtOfSessionInMinutes",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 902,
                column: "LenghtOfSessionInMinutes",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 903,
                column: "LenghtOfSessionInMinutes",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 904,
                column: "LenghtOfSessionInMinutes",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 905,
                column: "LenghtOfSessionInMinutes",
                value: 0.0);
        }
    }
}
