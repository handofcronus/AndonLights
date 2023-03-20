using AndonLights.DAL.Interfaces;
using AndonLights.Model;

namespace AndonLights.Repositories;

public class AndonLightRepository : IAndonLightRepo
{
    public IEnumerable<AndonLight> GetLights()
    {
        throw new NotImplementedException();
    }
}
