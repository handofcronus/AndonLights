
using AndonLights.DAL.Interfaces;

namespace AndonLights.Services;

public class AndonLightService
{

    private IAndonLightRepo _andonLightRepository;

    public AndonLightService(IAndonLightRepo andonLightRepository)
    {
        _andonLightRepository = andonLightRepository;
    }   
}
