using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace AndonLights.Migrations
{
    /// <inheritdoc />
    public partial class keyswap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NewKeyRequested",
                table: "Clients",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<ZonedDateTime>(
                name: "OldKeyDeadline",
                table: "Clients",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new NodaTime.ZonedDateTime(NodaTime.Instant.FromUnixTimeTicks(-621355968000000000L), NodaTime.TimeZones.TzdbDateTimeZoneSource.Default.ForId("UTC")));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewKeyRequested",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "OldKeyDeadline",
                table: "Clients");
        }
    }
}
