using AndonLights.Model;
using System.Collections;

namespace AndonLights.DAL.Interfaces;

public interface IAndonLightRepo
{
    public IEnumerable<AndonLight> GetLights();


}
