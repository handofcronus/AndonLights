using AndonLights.DTOs;
using AndonLights.Model;

namespace AndonLights.Mappers;

public static class StatMapper
{

    public static StatsBaseDTO toDto(this StatsBase statsBase)
    {
        return new StatsBaseDTO
        {
            DateOfStats = statsBase.DateOfStats.ToDateTimeUnspecified(),
            MinutesSpentInState = statsBase.MinutesSpentInState,
            NumberOfEntries = statsBase.NumberOfEntries,
        };
    }
}
