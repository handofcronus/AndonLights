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
        bool InDailyStats = false;
        DailyStateStats dailystateStats = null;
        foreach (var dailyStat in _dailyStats)
        {
            if (dailyStat.DayOfStats.Date == DateTime.Today)
            {
                dailystateStats = dailyStat;
                InDailyStats = true;
                break;
            }
        }
        var sessionsThisDay = GetSessionsFromADay(DateTime.Now, _closedSessions);
        int numEntries = 0;
        double minutesSpentInState = 0.0;
        foreach (var sessionThisDay in sessionsThisDay)
        {
            numEntries++;
            minutesSpentInState += sessionThisDay.LenghtOfSessionInMinutes;
        }
        
        if (InDailyStats)
        {
            dailystateStats.MinutesSpentInState = minutesSpentInState;
            dailystateStats.NumberOfEntries = numEntries;
        }
        else
        {
            dailystateStats= new DailyStateStats(numEntries,minutesSpentInState);
            _dailyStats.Add(dailystateStats);
        }
        
        
    }
    //tesztadatok
    public void updateMonthlyStats()
    {
        bool InMonthlyStats = false;
        MonthlyStateStats monthlyStateStats = null;
        foreach (var monthlyStat in _monthlyStats)
        {
            if (monthlyStat.MonthOfStats.Year == DateTime.Today.Year && monthlyStat.MonthOfStats.Month == DateTime.Today.Month)
            {
                monthlyStateStats = monthlyStat;
                InMonthlyStats = true;
                break;
            }
        }
        var sessionsThisMonth = GetSessionsFromAMonth(DateTime.Now, _closedSessions);
        int numEntries = 0;
        double minutesSpentInState = 0.0;
        foreach (var session in sessionsThisMonth)
        {
            numEntries++;
            minutesSpentInState += session.LenghtOfSessionInMinutes; 
        }
        
        if(InMonthlyStats)
        {
            monthlyStateStats.MinutesSpentInState = minutesSpentInState;
            monthlyStateStats.NumberOfEntries = numEntries;
        }
        else
        {
            monthlyStateStats = new MonthlyStateStats(numEntries,minutesSpentInState);
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
}
