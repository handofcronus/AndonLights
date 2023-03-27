using AndonLights.DTOs;

namespace AndonLights.Services.Interfaces;

public interface IStateService
{
    public StatsResponseDTO GetDailyStats(StatsQuestionDTO statsQuestion);
    public StatsResponseDTO GetMonthlyStats(StatsQuestionDTO statsQuestion);

}
