namespace AndonLights.Model;

public class MonthlyStateStats
{
 
    public int Id { get; set; }
    public int NumberOfEntries { get; set; }
    public double MinutesSpentInState { get; set; }
    public DateTime MonthOfStats { get;set;} 

    public MonthlyStateStats() 
    {
        NumberOfEntries = 0;
        MinutesSpentInState = 0.0;
    }

    public MonthlyStateStats(int numE, double minSpent)
    {
        NumberOfEntries = numE;
        MinutesSpentInState = minSpent;
    }

    public void Calc(List<Session> sessionsThisMonth)
    {
        int numEntries = sessionsThisMonth.Count;
        double minutesSpentInState = 0.0;
        foreach (var session in sessionsThisMonth)
        {
            minutesSpentInState += session.LenghtOfSessionInMinutes;
        }
        this.NumberOfEntries = numEntries;
        this.MinutesSpentInState = minutesSpentInState;
    }

}
