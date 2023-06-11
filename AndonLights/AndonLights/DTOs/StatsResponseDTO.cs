using AndonLights.Model;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace AndonLights.DTOs;

public class StatsResponseDTO
{
    
    public StatsBase? GreenStats {get; set;}
    public StatsBase? YellowStats {get; set;}
    public StatsBase? RedStats {get; set;}

    public StatsResponseDTO(StatsBase? green, StatsBase? yellow, StatsBase? red)
    {
        GreenStats = green;
        YellowStats = yellow;
        RedStats = red;
    }
}
