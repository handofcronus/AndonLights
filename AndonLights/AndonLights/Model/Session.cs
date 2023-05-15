using NodaTime;
using System.Diagnostics.CodeAnalysis;

namespace AndonLights.Model;

public class Session: IComparable<Session>
{
    public int StateId { get; set; }
    public int Id { get; set; }
    public double LenghtOfSessionInMinutes { get; set; }
    public required ZonedDateTime InTime { get; set; }
    public ZonedDateTime OutTime { get; set; }

    [SetsRequiredMembers]
    public Session(ZonedDateTime inTime)
    {
        InTime = inTime;
    }
    public Session()
    {
        
    }

    public void closeSession(ZonedDateTime outTime)
    {
        OutTime = outTime;
        Duration duration = OutTime - InTime;
        LenghtOfSessionInMinutes = duration.TotalMinutes;
        
    }

    public int CompareTo(Session? other)
    {
        return InTime.Date.CompareTo(other.InTime.Date);
    }

    
}
