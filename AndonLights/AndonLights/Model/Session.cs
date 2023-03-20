using System.Diagnostics.CodeAnalysis;

namespace AndonLights.Model;

public class Session
{
    public int Id { get; set; }
    public double LenghtOfSessionInMinutes { get; set; }
    public string? ErrorMessage { get; set; }
    public required DateTime InTime { get; set; }
    public DateTime OutTime { get; set; }

    [SetsRequiredMembers]
    public Session(DateTime inTime)
    {
        InTime = inTime;
    }
    public Session()
    {

    }

    public void closeSession(DateTime outTime)
    {
        OutTime = outTime;
        TimeSpan? ts = OutTime - InTime;
        if(ts.HasValue)
        {
            LenghtOfSessionInMinutes = ts.Value.TotalMinutes;
        }
        
    }
}
