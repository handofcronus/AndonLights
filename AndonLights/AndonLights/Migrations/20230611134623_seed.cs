using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AndonLights.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AndonLights",
                columns: new[] { "Id", "CurrentState", "DateOfCreation", "LastErrorMessage", "Name" },
                values: new object[,]
                {
                    { 901, "Green", new NodaTime.ZonedDateTime(NodaTime.Instant.FromUnixTimeTicks(16856142000000000L), NodaTime.TimeZones.TzdbDateTimeZoneSource.Default.ForId("UTC")), "", "LampWithStats" },
                    { 902, "Green", new NodaTime.ZonedDateTime(NodaTime.Instant.FromUnixTimeTicks(16856178000000000L), NodaTime.TimeZones.TzdbDateTimeZoneSource.Default.ForId("UTC")), "", "Seeded lamp1" },
                    { 903, "Green", new NodaTime.ZonedDateTime(NodaTime.Instant.FromUnixTimeTicks(16856214000000000L), NodaTime.TimeZones.TzdbDateTimeZoneSource.Default.ForId("UTC")), "", "Seeded lamp2" }
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "ID", "LightID", "StateColour" },
                values: new object[,]
                {
                    { 901, 901, "Green" },
                    { 902, 901, "Yellow" },
                    { 903, 901, "Red" },
                    { 904, 902, "Green" },
                    { 905, 902, "Yellow" },
                    { 906, 902, "Red" },
                    { 907, 903, "Green" },
                    { 908, 903, "Yellow" },
                    { 909, 903, "Red" }
                });

            migrationBuilder.InsertData(
                table: "Sessions",
                columns: new[] { "Id", "InTime", "LenghtOfSessionInMinutes", "OutTime", "StateId" },
                values: new object[,]
                {
                    { 901, new NodaTime.ZonedDateTime(NodaTime.Instant.FromUnixTimeTicks(16856142000000000L), NodaTime.TimeZones.TzdbDateTimeZoneSource.Default.ForId("UTC")), 0.0, new NodaTime.ZonedDateTime(NodaTime.Instant.FromUnixTimeTicks(16856195400000000L), NodaTime.TimeZones.TzdbDateTimeZoneSource.Default.ForId("UTC")), 901 },
                    { 902, new NodaTime.ZonedDateTime(NodaTime.Instant.FromUnixTimeTicks(16856195400000000L), NodaTime.TimeZones.TzdbDateTimeZoneSource.Default.ForId("UTC")), 0.0, new NodaTime.ZonedDateTime(NodaTime.Instant.FromUnixTimeTicks(16856699400000000L), NodaTime.TimeZones.TzdbDateTimeZoneSource.Default.ForId("UTC")), 902 },
                    { 903, new NodaTime.ZonedDateTime(NodaTime.Instant.FromUnixTimeTicks(16856699400000000L), NodaTime.TimeZones.TzdbDateTimeZoneSource.Default.ForId("UTC")), 0.0, new NodaTime.ZonedDateTime(NodaTime.Instant.FromUnixTimeTicks(16856930400000000L), NodaTime.TimeZones.TzdbDateTimeZoneSource.Default.ForId("UTC")), 901 },
                    { 904, new NodaTime.ZonedDateTime(NodaTime.Instant.FromUnixTimeTicks(16856930400000000L), NodaTime.TimeZones.TzdbDateTimeZoneSource.Default.ForId("UTC")), 0.0, new NodaTime.ZonedDateTime(NodaTime.Instant.FromUnixTimeTicks(16857059400000000L), NodaTime.TimeZones.TzdbDateTimeZoneSource.Default.ForId("UTC")), 902 },
                    { 905, new NodaTime.ZonedDateTime(NodaTime.Instant.FromUnixTimeTicks(16857059400000000L), NodaTime.TimeZones.TzdbDateTimeZoneSource.Default.ForId("UTC")), 0.0, new NodaTime.ZonedDateTime(NodaTime.Instant.FromUnixTimeTicks(16857132000000000L), NodaTime.TimeZones.TzdbDateTimeZoneSource.Default.ForId("UTC")), 903 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 901);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 902);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 903);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 904);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "Id",
                keyValue: 905);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 904);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 905);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 906);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 907);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 908);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 909);

            migrationBuilder.DeleteData(
                table: "AndonLights",
                keyColumn: "Id",
                keyValue: 902);

            migrationBuilder.DeleteData(
                table: "AndonLights",
                keyColumn: "Id",
                keyValue: 903);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 901);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 902);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "ID",
                keyValue: 903);

            migrationBuilder.DeleteData(
                table: "AndonLights",
                keyColumn: "Id",
                keyValue: 901);
        }
    }
}
