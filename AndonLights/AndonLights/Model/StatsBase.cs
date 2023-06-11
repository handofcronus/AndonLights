using NodaTime;

namespace AndonLights.Model;

public abstract class StatsBase
{
    public int Id { get; set; }
    public int StateId { get; set; }
    public int NumberOfEntries { get; set; }
    public double MinutesSpentInState { get; set; }
    public ZonedDateTime DateOfStats { get; set; }

    protected StatsBase()
    {

    }
    protected StatsBase(ZonedDateTime timeOfStat) 
    {
        NumberOfEntries = 0;
        MinutesSpentInState = 0.0;
        DateOfStats = timeOfStat;
    }
    public void Calc(List<Session> sessionsThisDay)
    {
        int numEntries = sessionsThisDay.Count;
        double minutesSpentInState = 0.0;
        foreach (var session in sessionsThisDay)
        {
            minutesSpentInState += session.LenghtOfSessionInMinutes;
        }
        this.NumberOfEntries = numEntries;
        this.MinutesSpentInState = minutesSpentInState;
    }

}
