
using AndonLights.DAL.Interfaces;
using AndonLights.DTOs;
using AndonLights.Model;

namespace AndonLights.Services;

public class AndonLightService :IAndonLightService
{

    private IAndonLightRepo _andonLightRepository;

    public AndonLightService(IAndonLightRepo andonLightRepository)
    {
        _andonLightRepository = andonLightRepository;
    }

    public AndonLightDTO GetLight(int id)
    {
        //repo helyett mock
        List<AndonLightDTO> lights = new List<AndonLightDTO>
        {
            new AndonLightDTO { ID = 1, Name = "test1", State = LightStates.Yellow },
            new AndonLightDTO { ID = 2, Name = "test2", State = LightStates.Blue }
        };
        return lights.Find(x => x.ID == id);
    }

    public IEnumerable<AndonLightDTO> GetLights()
    {

        //repo helyett mock
        List<AndonLightDTO> lights = new List<AndonLightDTO>
        {
            new AndonLightDTO { ID = 1, Name = "test1", State = LightStates.Yellow },
            new AndonLightDTO { ID = 2, Name = "test2", State = LightStates.Blue }
        };
        return lights;
    }

    public AndonLightDTO CreateLight(string name)
    {
        //repo helyett mock
        var light = new AndonLightDTO { Name = name, time = DateTime.Now, ID = 22, State = LightStates.Green };
        return light;
    }

    

    public bool DeleteLight(int id)
    {
        return _andonLightRepository.DeleteLight(id);
    }

    public AndonLightDTO UpdateLight(AndonLightDTO andonLight)
    {
        //repo helyett mock
        List<AndonLightDTO> lights = new List<AndonLightDTO>
        {
            new AndonLightDTO { ID = 1, Name = "test1", State = LightStates.Yellow,time = DateTime.MinValue },
            new AndonLightDTO { ID = 2, Name = "test2", State = LightStates.Blue,time = DateTime.MinValue }
        };
        var light = lights.Find(x => x.ID == andonLight.ID);
        light.State = andonLight.State;
        light.time = andonLight.time;
        light.Name = andonLight.Name;
        return light;
        //return _andonLightRepository.UpdateLight(andonLight);
    }

    private AndonLightDTO modelToDTO(AndonLight light)
    {
        return new AndonLightDTO { Name = light.Name, ID = light.Id, State = light.CurrentState };
    }

    private AndonLight DTOToModel(AndonLightDTO andonLight)
    {
        return new AndonLight(andonLight.Name) { Id = andonLight.ID, CurrentState = andonLight.State, DateOfCreation = andonLight.time };
    }
}
