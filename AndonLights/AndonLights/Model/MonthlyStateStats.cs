using NodaTime;

namespace AndonLights.Model;

public class MonthlyStateStats : StatsBase
{
    public MonthlyStateStats(ZonedDateTime time) :base(time) { }
    public MonthlyStateStats()
    {

    }
}
