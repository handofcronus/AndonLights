using AndonLights.DAL.Interfaces;
using AndonLights.DTOs;

namespace AndonLights.Repositories;

public class StateRepository : IStateRepo
{
    public StatsResponseDTO GetDailyStats(StatsQuestionDTO statsQuestion)
    {
        throw new NotImplementedException();
    }

    public StatsResponseDTO GetMonthlyStats(StatsQuestionDTO statsQuestion)
    {
        throw new NotImplementedException();
    }
}
