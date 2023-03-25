namespace AndonLights.Model;

public class DailyStateStats
{
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
}
