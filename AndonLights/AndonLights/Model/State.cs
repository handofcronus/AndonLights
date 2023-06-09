﻿
using NodaTime;

namespace AndonLights.Model;

public class State
{
    public int LightID { get; set; }
    public int ID { get; set; }
    public List<Session> ClosedSessions { get; } = new List<Session>();

   // private Session _currentSession;
    public LightStates StateColour { get; set; }
    public List<MonthlyStateStats> MonthlyStats { get; }
    public List<DailyStateStats> DailyStats { get; }

    public string GetLastErrorMessage()
    {
        var lastSession = getLastSession();
        return lastSession.ErrorMessage??"";
    }

    public State(LightStates StateColour)
    {
        ClosedSessions = new List<Session>();
        MonthlyStats = new List<MonthlyStateStats>();
        DailyStats = new List<DailyStateStats>();
        this.StateColour = StateColour;

    }

    public void UpdateDailyStats()
    {
        DailyStateStats todaysStats = GetADailyStatOrDefault(new ZonedDateTime(SystemClock.Instance.GetCurrentInstant(), DateTimeZone.Utc));
        var sessionsThisDay = GetSessionsFromADay(new ZonedDateTime(SystemClock.Instance.GetCurrentInstant(), DateTimeZone.Utc), ClosedSessions);
        todaysStats.Calc(sessionsThisDay);
        if (!DailyStats.Contains(todaysStats))
        {
            DailyStats.Add(todaysStats);
        }
    }

    public void UpdateMonthlyStats()
    {
        MonthlyStateStats monthlyStateStats = GetAMonthlyStatsOrDefault(new ZonedDateTime(SystemClock.Instance.GetCurrentInstant(), DateTimeZone.Utc));
        var sessionsThisMonth = GetSessionsFromAMonth(new ZonedDateTime(SystemClock.Instance.GetCurrentInstant(), DateTimeZone.Utc), ClosedSessions);
        monthlyStateStats.Calc(sessionsThisMonth);
        if(!MonthlyStats.Contains(monthlyStateStats))
        {
            MonthlyStats.Add(monthlyStateStats);
        }
    }

    public DailyStateStats? GetDailyStats(ZonedDateTime time)
    {
        return DailyStats.Find(x => x.DateOfStats.Date == time.Date);
    }
    public MonthlyStateStats? GetMonthlyStats(ZonedDateTime time)
    {
        return MonthlyStats.Find(x => x.DateOfStats.Date.Year == time.Date.Year && x.DateOfStats.Month == time.Date.Month);
    }


    public void ActivateState(string errorMessage)
    {
        var currentSession = new Session(new ZonedDateTime(SystemClock.Instance.GetCurrentInstant(), DateTimeZone.Utc),errorMessage);
        ClosedSessions.Add(currentSession);
    }

    public void CloseState()
    {
        if(ClosedSessions.Count == 0)
        {
            return;
        }
        ClosedSessions.Sort();
        var lastSession = ClosedSessions.Last();
        lastSession.closeSession(new ZonedDateTime(SystemClock.Instance.GetCurrentInstant(), DateTimeZone.Utc));
    }

    private List<Session> GetSessionsFromADay(ZonedDateTime date, List<Session> closedSessions)
    {
        return closedSessions.FindAll(x => x.InTime.Date == date.Date);
    }
    private List<Session> GetSessionsFromAMonth(ZonedDateTime date, List<Session> closedSessions)
    {
        return closedSessions.FindAll(x => x.InTime.Date.Year == date.Date.Year && x.InTime.Date.Month == date.Date.Month);
    }

    private DailyStateStats GetADailyStatOrDefault(ZonedDateTime dateTime)
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
    private MonthlyStateStats GetAMonthlyStatsOrDefault(ZonedDateTime dateTime)
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

    private Session getLastSession()
    {
        ClosedSessions.Sort();
        return ClosedSessions.Last();
    }
}
