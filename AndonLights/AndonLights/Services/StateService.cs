using AndonLights.DAL.Repositories.Interfaces;
using AndonLights.DTOs;
using AndonLights.Services.Interfaces;

namespace AndonLights.Services;

public class StateService : IStateService
{
    private readonly IStateRepo _stateRepo;

    public StateService(IStateRepo stateServiceRepo)
    {
        _stateRepo = stateServiceRepo;
    }

    public StatsResponseDTO GetDailyStats(StatsQuestionDTO statsQuestion)
    {
        UpdateAllDailyStats();
        return _stateRepo.GetDailyStats(statsQuestion);
    }

    public StatsResponseDTO GetMonthlyStats(StatsQuestionDTO statsQuestion)
    {
        UpdateAllMonthlyStats();
        return _stateRepo.GetMonthlyStats(statsQuestion);
    }

    public void UpdateAllDailyStats()
    {
        var states = _stateRepo.GetAllStates();
        foreach (var state in states) 
        {
            state.UpdateDailyStats();
        }
        _stateRepo.SaveDb();
    }

    public void UpdateAllMonthlyStats()
    {
        var states = _stateRepo.GetAllStates();
        foreach (var state in states)
        {
            state.UpdateMonthlyStats();
        }
        _stateRepo.SaveDb();
    }
}
