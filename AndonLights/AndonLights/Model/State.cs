using System.Linq.Expressions;

namespace AndonLights.Model;

public class State
{
    public int LightID { get; set; }
    public int ID { get; set; }
    public List<Session> ClosedSessions { get; } = new List<Session>();

    private Session _currentSession;
    public LightStates StateColour { get; set; }
    public List<MonthlyStateStats> MonthlyStats { get; }
    public List<DailyStateStats> DailyStats { get; }

    public State(LightStates StateColour)
    {
        ClosedSessions = new List<Session>();
        MonthlyStats = new List<MonthlyStateStats>();
        DailyStats = new List<DailyStateStats>();
        _currentSession = new Session(DateTime.Now);
        this.StateColour = StateColour;

    }

    public void updateDailyStats()
    {
        DailyStateStats todaysStats = GetADailyStatOrDefault(DateTime.Today);
        var sessionsThisDay = GetSessionsFromADay(DateTime.Now, ClosedSessions);
        todaysStats.Calc(sessionsThisDay);
        if (!DailyStats.Contains(todaysStats))
        {
            DailyStats.Add(todaysStats);
        }
        Console.WriteLine("updateDailyStats");
    }

    public void updateMonthlyStats()
    {
        MonthlyStateStats monthlyStateStats = GetAMonthlyStatsOrDefault(DateTime.Today);
        var sessionsThisMonth = GetSessionsFromAMonth(DateTime.Now, ClosedSessions);
        monthlyStateStats.Calc(sessionsThisMonth);
        if(!MonthlyStats.Contains(monthlyStateStats))
        {
            MonthlyStats.Add(monthlyStateStats);
        }
    }

    public DailyStateStats? GetDailyStats(DateTime time)
    {
        return DailyStats.Find(x => x.DateOfStats.Date == time.Date);
    }
    public MonthlyStateStats? GetMonthlyStats(DateTime time)
    {
        return MonthlyStats.Find(x => x.DateOfStats.Date == time.Date);
    }


    public void activateState(DateTime timeOfSwitch)
    {
        _currentSession = new Session(timeOfSwitch);
    }

    public void closeState(DateTime timeOfSwitch)
    {
        _currentSession.closeSession(timeOfSwitch);
        ClosedSessions.Add(_currentSession);
    }

    private List<Session> GetSessionsFromADay(DateTime date, List<Session> closedSessions)
    {
        return closedSessions.FindAll(x => x.InTime.Date == date.Date);
    }
    private List<Session> GetSessionsFromAMonth(DateTime date, List<Session> closedSessions)
    {
        return closedSessions.FindAll(x => x.InTime.Date.Year == date.Date.Year && x.InTime.Date.Month == date.Date.Month);
    }

    private DailyStateStats GetADailyStatOrDefault(DateTime dateTime)
    {
        foreach (var dailyStat in DailyStats)
        {
            if (dailyStat.DateOfStats.Date == dateTime.Date)
            {
                return dailyStat;
            }
        }
        return new DailyStateStats();
    }
    private MonthlyStateStats GetAMonthlyStatsOrDefault(DateTime dateTime)
    {
        foreach (var monthlyStat in MonthlyStats)
        {
            if (monthlyStat.DateOfStats.Date.Year == dateTime.Year && monthlyStat.DateOfStats.Month == dateTime.Month)
            {
                return monthlyStat;
            }
        }
        return new MonthlyStateStats();
    }
}
