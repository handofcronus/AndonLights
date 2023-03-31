namespace AndonLights.Model;

public class DailyStateStats
{
    public int Id { get; set; }
    public int NumberOfEntries { get; set; }
    public double MinutesSpentInState { get; set; }
    public DateTime DayOfStats { get; set; }
    
    public DailyStateStats()
    {
        NumberOfEntries = 0;
        MinutesSpentInState = 0.0;
    }
    public DailyStateStats(int numE, double minSpent)
    {
        NumberOfEntries = numE;
        MinutesSpentInState = minSpent;
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
