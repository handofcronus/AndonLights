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
    public void createDailyStats()
    {

    }

    public void createMonthlyStats()
    {

    }

    public void activateState(DateTime timeOfSwitch)
    {
        Session session = new Session() {InTime=timeOfSwitch,OutTime=DateTime.Now };
        _currentSession = session;
        
    }

    public void closeState(DateTime timeOfSwitch)
    {
        _currentSession.closeSession(timeOfSwitch);
        
    }

}
