using AndonLights.DTOs;
using AndonLights.Model;
using NodaTime;

namespace AndonLights.Mappers;

public static class AndonStateMapper
{
    public static AndonLight ToModel(this AndonStateDTO dto)
    {
        return new AndonLight("dto")
        {
            Id = dto.id,
            DateOfCreation = new ZonedDateTime(new Instant(), DateTimeZone.Utc),
            CurrentState = LightStateHelper.FromString(dto.state),
        };
    }
}