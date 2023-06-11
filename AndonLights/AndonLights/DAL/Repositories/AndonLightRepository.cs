using AndonLights.DAL;
using AndonLights.DAL.Repositories.Interfaces;
using AndonLights.DTOs;
using AndonLights.Model;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace AndonLights.Repositories;

public class AndonLightRepository : IAndonLightRepo
{
    private readonly AndonLightsDbContext _dbContext;
    private readonly ILogger<AndonLightRepository> _logger;

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

    public AndonLight GetLightByIdWithChildren(int lightId)
    {
        return _dbContext.AndonLights
            .Include(a => a.States).ThenInclude(s => s.DailyStats)
            .Include(a => a.States).ThenInclude(s => s.MonthlyStats)
            .Include(a => a.States).ThenInclude(s => s.ClosedSessions)
            .Single(x => x.Id == lightId);
    }

    public IEnumerable<AndonLight> GetLightsWithoutChildren()
    {
        return _dbContext.AndonLights.ToList();
    }

    public AndonLight GetLightByIdWithoutChildren(int id)
    {
        return _dbContext.AndonLights.Single(a => a.Id == id);
    }

    

    public AndonLight Insert(string name)
    {
        AndonLight light = new AndonLight(name) 
        {
            DateOfCreation = new ZonedDateTime(SystemClock.Instance.GetCurrentInstant(),DateTimeZone.Utc),
            CurrentState = LightStates.Green 
        };
        _dbContext.AndonLights.Add(light);
        _dbContext.SaveChanges();
        return light;
    }

    public AndonLight SwitchState(AndonLight param,string errorMessage)
    {
        var light = GetLightByIdWithChildren(param.Id);
        light.SwitchedState(param.CurrentState,errorMessage);
        _dbContext.SaveChanges();
        return light;
    }

    public AndonLight? UpdateLight(UpdateLightDTO andonLight)
    {
        var light = _dbContext.AndonLights.Single(x => x.Id == andonLight.Id);
        if (light is null)
        {
            return null;
        }
        else
        {
            light.Name= andonLight.Name;
        }
        _dbContext.SaveChanges();
        return light;
    }

    public IEnumerable<AndonLight> GetLightsWithChildren()
    {
        return _dbContext.AndonLights
            .Include(a => a.States).ThenInclude(s => s.DailyStats)
            .Include(a => a.States).ThenInclude(s => s.MonthlyStats)
            .Include(a => a.States).ThenInclude(s => s.ClosedSessions).ToList();
    }
}
