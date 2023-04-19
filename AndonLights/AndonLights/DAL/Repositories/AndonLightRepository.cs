using AndonLights.DAL;
using AndonLights.DAL.Repositories.Interfaces;
using AndonLights.DTOs;
using AndonLights.Model;
using Microsoft.EntityFrameworkCore;

namespace AndonLights.Repositories;

public class AndonLightRepository : IAndonLightRepo
{
    private AndonLightsDbContext _dbContext;
    private ILogger<AndonLightRepository> _logger;

    public AndonLightRepository(AndonLightsDbContext dbContext, ILogger<AndonLightRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
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
        return _dbContext.AndonLights
            .Include(a => a.States).ThenInclude(s => s.DailyStats)
            .Include(a => a.States).ThenInclude(s => s.MonthlyStats)
            .Include(a => a.States).ThenInclude(s => s.ClosedSessions)
            .Single(x => x.Id == lightId);
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
        try
        {
            light.SwitchedState(andonLightDTO);
        }
        catch(InvalidOperationException)
        {
            _logger.LogInformation($"Light with {light.Id} id is not in a valid state.");
            throw new InvalidOperationException();
        }
        
        _dbContext.SaveChanges();
        return light;


    }
}
