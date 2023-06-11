using AndonLights.DAL.Repositories.Interfaces;
using AndonLights.DTOs;
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
        return modelToLightDTO(_andonLightRepository.GetLightByIdWithChildren(id));
    }

    public IEnumerable<AndonLightDTO> GetLights()
    {
        return _andonLightRepository.GetLightsWithoutChildren().Select(modelToLightDTO).ToList();
    }

    public AndonLightDTO CreateLight(string name)
    {
        var light = _andonLightRepository.Insert(name);
        return modelToLightDTO(light);
    }



    public bool DeleteLight(int id)
    {
        return _andonLightRepository.DeleteLight(id);
    }

    public AndonLightDTO? UpdateLight(AndonLightDTO dto)
    {
        var light = _andonLightRepository.UpdateLight(dtoLightToModel(dto));
        if (light is null)
        {
            return null;
        }
        return modelToLightDTO(light);
    }

    public AndonStateDTO SwitchState(AndonStateDTO dto)
    {
        return modelToStateDTO(_andonLightRepository.SwitchState(dtoStateToModel(dto),dto.ErrorMessage ?? ""));
    }
    public AndonStateDTO GetState(int id)
    {
        var lights = _andonLightRepository.GetLightByIdWithChildren(id);
        return modelToStateDTO(lights);
    }

    public IEnumerable<AndonStateDTO> GetStates()
    {
        return _andonLightRepository.GetLightsWithChildren().Select(light => modelToStateDTO(light));
    }


    private AndonLightDTO modelToLightDTO(AndonLight light)
    {
        
        LocalDate dOC = light.DateOfCreation.Date;
        return new AndonLightDTO(light.Name) { ID = light.Id,
            StateCode = light.CurrentState,
            State= LightStateHelper.ToString(light.CurrentState),
            ErrorMessage = light.GetLastErrorMessage(),
            DateofCreation = new DateTime(dOC.Year,dOC.Month,dOC.Day)};
    }

    private AndonLight dtoLightToModel(AndonLightDTO dto)
    {
        return new AndonLight(dto.Name)
        {
            Id = dto.ID,
            CurrentState = dto.StateCode,
            DateOfCreation = new ZonedDateTime(Instant.FromDateTimeUtc(dto.DateofCreation), DateTimeZone.Utc)
        };
    }
    private AndonLight dtoStateToModel(AndonStateDTO dto)
    {
        return new AndonLight("dto")
        {
            Id = dto.id,
            DateOfCreation = new ZonedDateTime(new Instant(), DateTimeZone.Utc),
            CurrentState = LightStateHelper.FromString(dto.state),
        };
    }

    private AndonStateDTO modelToStateDTO(AndonLight light)
    {
        return new AndonStateDTO(light.Id, LightStateHelper.ToString(light.CurrentState))
        {
            ErrorMessage = light.States.Count==0 ? "" : light.GetLastErrorMessage()
        };
    }

}
