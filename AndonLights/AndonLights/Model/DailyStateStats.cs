using NodaTime;

namespace AndonLights.Model;

public class DailyStateStats : StatsBase
{
    public DailyStateStats(ZonedDateTime time) : base(time) { }
    public DailyStateStats()
    {

    }
}
