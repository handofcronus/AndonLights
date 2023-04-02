namespace AndonLights.Model;

public class StatsBase
{
    public int Id { get; set; }
    public int NumberOfEntries { get; set; }
    public double MinutesSpentInState { get; set; }
    public DateTime DateOfStats { get; set; }

    protected StatsBase() 
    {
        NumberOfEntries = 0;
        MinutesSpentInState = 0.0;
        DateOfStats = DateTime.Now;
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
