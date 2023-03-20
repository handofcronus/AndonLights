using AndonLights.DAL.Interfaces;

namespace AndonLights.Services;

public class SessionService
{

    private ISessionRepo _sessionRepo;

    public SessionService(ISessionRepo sessionRepo)
    {
        _sessionRepo = sessionRepo;
    }



}
