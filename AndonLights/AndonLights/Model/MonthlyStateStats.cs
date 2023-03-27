namespace AndonLights.Model;

public class MonthlyStateStats
{
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
}
