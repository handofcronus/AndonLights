using AndonLights.DTOs;
using AndonLights.Model;
using System.Collections;

namespace AndonLights.DAL.Repositories.Interfaces;

public interface IAndonLightRepo
{
    public IEnumerable<AndonLight> GetLights();

    public AndonLight GetLightById(int lightId);
    public AndonLight Insert(string name);
    public bool DeleteLight(int id);
    public AndonLight? UpdateLight(AndonLightDTO andonLight);

}
