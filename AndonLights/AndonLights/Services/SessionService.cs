using AndonLights.DAL.Repositories.Interfaces;
using AndonLights.Services.Interfaces;

namespace AndonLights.Services;

public class SessionService :ISessionService
{

    private ISessionRepo _sessionRepo;

    public SessionService(ISessionRepo sessionRepo)
    {
        _sessionRepo = sessionRepo;
    }



}
