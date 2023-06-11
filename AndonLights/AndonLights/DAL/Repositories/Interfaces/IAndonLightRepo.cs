using AndonLights.DTOs;
using AndonLights.Model;
using System.Collections;

namespace AndonLights.DAL.Repositories.Interfaces;

public interface IAndonLightRepo
{
    public IEnumerable<AndonLight> GetLightsWithChildren();
    public IEnumerable<AndonLight> GetLightsWithoutChildren();
    public AndonLight GetLightByIdWithChildren(int lightId);
    public AndonLight GetLightByIdWithoutChildren(int id);
    public AndonLight Insert(string name);
    public bool DeleteLight(int id);
    public AndonLight? UpdateLight(AndonLight andonLight);
    public AndonLight SwitchState(AndonLight param,string errorMessage);

    
    

}
