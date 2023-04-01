using AndonLights.DAL;
using AndonLights.DAL.Repositories.Interfaces;
using AndonLights.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AndonLights.Repositories;

public class StateRepository : IStateRepo
{
    private AndonLightsDbContext _dbContext;


    public StateRepository(AndonLightsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public StatsResponseDTO GetDailyStats(StatsQuestionDTO statsQuestion)
    {
        var light = _dbContext.AndonLights
            .Include(l => l.States)
            .ThenInclude(s => s.DailyStats)
            .Single(x => x.Id == statsQuestion.id);
        return light.GetDailyStatsFromStates(statsQuestion);
    }

    public StatsResponseDTO GetMonthlyStats(StatsQuestionDTO statsQuestion)
    {
        var light = _dbContext.AndonLights
            .Include(l => l.States)
            .ThenInclude(s => s.MonthlyStats)
            .Single(x => x.Id == statsQuestion.id);
        return light.GetMonthlyStatsFromStates(statsQuestion);
    }
}
