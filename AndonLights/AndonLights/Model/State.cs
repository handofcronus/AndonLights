using System.Linq.Expressions;

namespace AndonLights.Model;

public class State
{
    private int id;
    private List<Session> _closedSessions;
    private Session _currentSession;
    private List<MonthlyStateStats> _monthlyStats;
    private List<DailyStateStats> _dailyStats;

    public State()
    {
        _closedSessions = new List<Session>();
        _monthlyStats = new List<MonthlyStateStats>();
        _dailyStats = new List<DailyStateStats>();
        _currentSession = new Session(DateTime.MinValue);
    }

    //tesztadatok
    public void updateDailyStats()
    {
        DailyStateStats todaysStats = GetADailyStatOrDefault(DateTime.Today);
        var sessionsThisDay = GetSessionsFromADay(DateTime.Now, _closedSessions);
        int numEntries = 0;
        double minutesSpentInState = 0.0;
        foreach (var sessionThisDay in sessionsThisDay)
        {
            numEntries++;
            minutesSpentInState += sessionThisDay.LenghtOfSessionInMinutes;
        }
        todaysStats.MinutesSpentInState = minutesSpentInState;
        todaysStats.NumberOfEntries = numEntries;
        if (!_dailyStats.Contains(todaysStats))
        {
            _dailyStats.Add(todaysStats);
        }
    }
    //tesztadatok
    public void updateMonthlyStats()
    {
        MonthlyStateStats monthlyStateStats = GetAMonthlyStatsOrDefault(DateTime.Today);
        var sessionsThisMonth = GetSessionsFromAMonth(DateTime.Now, _closedSessions);
        int numEntries = 0;
        double minutesSpentInState = 0.0;
        foreach (var session in sessionsThisMonth)
        {
            numEntries++;
            minutesSpentInState += session.LenghtOfSessionInMinutes; 
        }
        monthlyStateStats.NumberOfEntries = numEntries;
        monthlyStateStats.MinutesSpentInState = minutesSpentInState;
        if(!_monthlyStats.Contains(monthlyStateStats))
        {
            _monthlyStats.Add(monthlyStateStats);
        }
    }

    public void activateState(DateTime timeOfSwitch)
    {
        Session session = new Session() { InTime = timeOfSwitch, OutTime = DateTime.Now };
        _currentSession = session;

    }

    public void closeState(DateTime timeOfSwitch)
    {
        _currentSession.closeSession(timeOfSwitch);
        _closedSessions.Add(_currentSession);

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
        foreach (var dailyStat in _dailyStats)
        {
            if (dailyStat.DayOfStats.Date == dateTime.Date)
            {
                return dailyStat;
            }
        }
        return new DailyStateStats();
    }
    private MonthlyStateStats GetAMonthlyStatsOrDefault(DateTime dateTime)
    {
        foreach (var monthlyStat in _monthlyStats)
        {
            if (monthlyStat.MonthOfStats.Date.Year == dateTime.Year && monthlyStat.MonthOfStats.Month == dateTime.Month)
            {
                return monthlyStat;
            }
        }
        return new MonthlyStateStats();
    }
}
