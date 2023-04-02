using AndonLights.DAL;
using AndonLights.DAL.Repositories.Interfaces;
using AndonLights.DTOs;
using AndonLights.Model;
using AndonLights.Repositories;
using AndonLights.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AndonLights.Services;

public class AndonLightService :IAndonLightService
{

    private IAndonLightRepo _andonLightRepository;

    public AndonLightService(IAndonLightRepo andonLightRepository, AndonLightsDbContext andonLightsDbContext)
    {
        _andonLightRepository = andonLightRepository;
    }

    public AndonLightDTO GetLight(int id)
    {
        return modelToDTO(_andonLightRepository.GetLightById(id));
    }

    public IEnumerable<AndonLightDTO> GetLights()
    {
        return _andonLightRepository.GetLights().Select(modelToDTO).ToList();
    }

    public AndonLightDTO CreateLight(string name)
    {
        var light = _andonLightRepository.Insert(name);
        return modelToDTO(light);
    }

    

    public bool DeleteLight(int id)
    {
        return _andonLightRepository.DeleteLight(id);
    }

    public AndonLightDTO? UpdateLight(AndonLightDTO andonLight)
    {
        var light = _andonLightRepository.UpdateLight(andonLight);
        if (light is null)
        {
            return null;
        }
        return modelToDTO(light);
    }

    private AndonLightDTO modelToDTO(AndonLight light)
    {
        return new AndonLightDTO(light.Name) { ID = light.Id, State = light.CurrentState };
    }

    private AndonLight DTOToModel(AndonLightDTO andonLight)
    {
        return new AndonLight(andonLight.Name) { Id = andonLight.ID, CurrentState = andonLight.State, DateOfCreation = andonLight.time };
    }
}
