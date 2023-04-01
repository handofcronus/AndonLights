using AndonLights.DAL;
using AndonLights.DAL.Repositories.Interfaces;
using AndonLights.DTOs;
using AndonLights.Model;

namespace AndonLights.Repositories;

public class AndonLightRepository : IAndonLightRepo
{
    private AndonLightsDbContext _dbContext;


    public AndonLightRepository(AndonLightsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool DeleteLight(int id)
    {
        var light = _dbContext.AndonLights.Single(x => x.Id == id);
        if(light == null) 
        {
            return false;
        }
        _dbContext.Remove(light);
        _dbContext.SaveChanges();
        return true;
    }

    public AndonLight GetLightById(int lightId)
    {
        return _dbContext.AndonLights.Single(x => x.Id == lightId);
    }

    public IEnumerable<AndonLight> GetLights()
    {
        return _dbContext.AndonLights.ToList();
    }

    public AndonLight Insert(string name)
    {
        AndonLight light = new AndonLight(name) {DateOfCreation = DateTime.Now,CurrentState = LightStates.Green };
        _dbContext.AndonLights.Add(light);
        _dbContext.SaveChanges();
        return light;
    }

    public AndonLight? UpdateLight(AndonLightDTO andonLightDTO)
    {
        var light = _dbContext.AndonLights.Single(x => x.Id == andonLightDTO.ID);
        if (light is null)
        {
            return null;
        }
        light.SwitchedState(andonLightDTO);
        _dbContext.SaveChanges();
        return light;


    }
}
