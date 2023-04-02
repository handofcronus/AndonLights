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
        return _dbContext.States.ToList();
    }

    public StatsResponseDTO GetDailyStats(StatsQuestionDTO statsQuestion)
    {
        var light = _lightRepository.GetLightById(statsQuestion.id);
        return light.GetDailyStatsFromStates(statsQuestion);
    }

    public StatsResponseDTO GetMonthlyStats(StatsQuestionDTO statsQuestion)
    {
        var light = _lightRepository.GetLightById(statsQuestion.id);
        return light.GetMonthlyStatsFromStates(statsQuestion);
    }
}
