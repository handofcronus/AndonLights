using AndonLights.DAL;
using AndonLights.DAL.Interfaces;
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
        throw new NotImplementedException();
    }

    public AndonLight GetLightById(int lightId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<AndonLight> GetLights()
    {
        throw new NotImplementedException();
    }

    public AndonLight Insert(string name)
    {
        //AndonLight light = new AndonLight() {Name = name,DateOfCreation = DateTime.Now,CurrentState = LightStates.Green };
        //_dbContext.Lights.Add(light);
        throw new NotImplementedException();
    }

    public AndonLight UpdateLight(AndonLightDTO andonLight)
    {
        throw new NotImplementedException();
    }
}
