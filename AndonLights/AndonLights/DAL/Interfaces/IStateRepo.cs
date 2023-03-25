﻿using AndonLights.DTOs;

namespace AndonLights.DAL.Interfaces;

public interface IStateRepo
{
    public StatsResponseDTO GetDailyStats(StatsQuestionDTO statsQuestion);
    public StatsResponseDTO GetMonthlyStats(StatsQuestionDTO statsQuestion);




}
