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
        throw new NotImplementedException();
    }

    public StatsResponseDTO GetMonthlyStats(StatsQuestionDTO statsQuestion)
    {
        throw new NotImplementedException();
    }
}
