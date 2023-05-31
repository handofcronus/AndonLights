using AndonLights.DAL;
using AndonLights.DAL.Repositories.Interfaces;
using AndonLights.DTOs;
using AndonLights.Model;
using Microsoft.EntityFrameworkCore;

namespace AndonLights.Repositories;

public class StateRepository : IStateRepo
{
    private AndonLightsDbContext _dbContext;
    private IAndonLightRepo _lightRepository;

    public StateRepository(AndonLightsDbContext dbContext, IAndonLightRepo andonLightRepository)
    {
        _dbContext = dbContext;
        _lightRepository = andonLightRepository;
    }

    public List<State> GetAllStates()
    {
        return _dbContext.States.Include(s=>s.ClosedSessions).Include(s=>s.DailyStats).Include(s=>s.MonthlyStats).ToList();
    }

    public StatsResponseDTO GetDailyStats(StatsQuestionDTO statsQuestion)
    {
        var light = _lightRepository.GetLightByIdWithChildren(statsQuestion.Id);
        return light.GetDailyStatsFromStates(statsQuestion);
    }

    public StatsResponseDTO GetMonthlyStats(StatsQuestionDTO statsQuestion)
    {
        var light = _lightRepository.GetLightByIdWithChildren(statsQuestion.Id);
        return light.GetMonthlyStatsFromStates(statsQuestion);
    }
    public void SaveDb()
    {
        _dbContext.SaveChanges();
    }

}
