using AndonLights.Model;
using NodaTime;

namespace AndonLights.DTOs;

public class AndonLightDTO
{
    public int ID { get; set; }
    public string Name { get; set; }
    public LightStates State { get; set; }
    public string? ErrorMessage { get; set; }

    public ZonedDateTime time { get; set; } = new ZonedDateTime( SystemClock.Instance.GetCurrentInstant(),DateTimeZone.Utc);

    public AndonLightDTO(string name)
    {
        Name = name;
    }
}
