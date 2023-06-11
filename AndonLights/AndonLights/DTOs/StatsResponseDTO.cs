using AndonLights.Mappers;
using AndonLights.Model;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using NodaTime;

namespace AndonLights.DTOs;

public class StatsResponseDTO
{
    
    public StatsBaseDTO? GreenStats {get; set;}
    public StatsBaseDTO? YellowStats {get; set;}
    public StatsBaseDTO? RedStats {get; set;}

    public StatsResponseDTO(StatsBase? green, StatsBase? yellow, StatsBase? red)
    {
        GreenStats = green?.toDto();
        YellowStats = yellow?.toDto();
        RedStats = red?.toDto();
    }
}

public class StatsBaseDTO
{
    public int NumberOfEntries { get; set; }
    public double MinutesSpentInState { get; set; }
    public DateTime DateOfStats { get; set; }
}
