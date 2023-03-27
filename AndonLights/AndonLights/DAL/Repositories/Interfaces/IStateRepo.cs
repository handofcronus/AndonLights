using AndonLights.DTOs;

namespace AndonLights.DAL.Repositories.Interfaces;

public interface IStateRepo
{
    public StatsResponseDTO GetDailyStats(StatsQuestionDTO statsQuestion);
    public StatsResponseDTO GetMonthlyStats(StatsQuestionDTO statsQuestion);




}
