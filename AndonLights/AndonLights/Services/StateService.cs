﻿using AndonLights.DAL.Repositories.Interfaces;
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
