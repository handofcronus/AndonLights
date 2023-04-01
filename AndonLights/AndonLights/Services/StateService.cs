using AndonLights.DAL.Repositories.Interfaces;
using AndonLights.DTOs;
using AndonLights.Services.Interfaces;

namespace AndonLights.Services;

public class StateService : IStateService
{
    private IStateRepo _stateRepo;

    public StateService(IStateRepo stateServiceRepo)
    {
        _stateRepo = stateServiceRepo;
    }

    public StatsResponseDTO GetDailyStats(StatsQuestionDTO statsQuestion)
    {
        return _stateRepo.GetDailyStats(statsQuestion);
    }

    public StatsResponseDTO GetMonthlyStats(StatsQuestionDTO statsQuestion)
    {
        return _stateRepo.GetMonthlyStats(statsQuestion);
    }
}
