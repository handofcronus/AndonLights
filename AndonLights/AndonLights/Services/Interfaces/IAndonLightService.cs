using AndonLights.DTOs;
using AndonLights.Model;

namespace AndonLights.Services.Interfaces;

public interface IAndonLightService
{
    public IEnumerable<AndonLightDTO> GetLights();
    public AndonLightDTO GetLight(int id);
    public AndonLightDTO CreateLight(string name);

    public bool DeleteLight(int id);

    public AndonLightDTO? UpdateLight(UpdateLightDTO dto);

    public AndonStateDTO GetState(int id);
    public IEnumerable<AndonStateDTO> GetStates();
    public AndonStateDTO SwitchState(AndonStateDTO dto);
}
