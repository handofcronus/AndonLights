using AndonLights.DAL.Repositories.Interfaces;
using AndonLights.DTOs;
using AndonLights.Mappers;
using AndonLights.Model;
using AndonLights.Services.Interfaces;
using NodaTime;

namespace AndonLights.Services;

public class AndonLightService : IAndonLightService
{

    private IAndonLightRepo _andonLightRepository;
    private ILogger<AndonLightService> _logger;

    public AndonLightService(IAndonLightRepo andonLightRepository, ILogger<AndonLightService> logger)
    {
        _andonLightRepository = andonLightRepository;
        _logger = logger;
    }

    public AndonLightDTO GetLight(int id)
    {
        return _andonLightRepository.GetLightByIdWithChildren(id).ToLightDTO();
    }

    public IEnumerable<AndonLightDTO> GetLights()
    {
        return _andonLightRepository.GetLightsWithoutChildren().ToList<AndonLight>().ToLightDTO();
    }

    public AndonLightDTO CreateLight(string name)
    {
        var light = _andonLightRepository.Insert(name);
        return light.ToLightDTO();
    }



    public bool DeleteLight(int id)
    {
        return _andonLightRepository.DeleteLight(id);
    }

    public AndonLightDTO? UpdateLight(AndonLightDTO dto)
    {
        var light = _andonLightRepository.UpdateLight(dto.ToModel());
        if (light is null)
        {
            return null;
        }
        return light.ToLightDTO();
    }

    public AndonStateDTO SwitchState(AndonStateDTO dto)
    {
        return _andonLightRepository.SwitchState(dto.ToModel(),dto.ErrorMessage ?? "").ToStateDTO();
    }
    public AndonStateDTO GetState(int id)
    {
        var lights = _andonLightRepository.GetLightByIdWithChildren(id);
        return lights.ToStateDTO();
    }

    public IEnumerable<AndonStateDTO> GetStates()
    {
        return _andonLightRepository.GetLightsWithChildren().ToList<AndonLight>().ToStateDTO();
    }
}
