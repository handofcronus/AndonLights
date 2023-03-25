using AndonLights.DAL.Interfaces;

namespace AndonLights.Services;

public class SessionService :ISessionService
{

    private ISessionRepo _sessionRepo;

    public SessionService(ISessionRepo sessionRepo)
    {
        _sessionRepo = sessionRepo;
    }



}
