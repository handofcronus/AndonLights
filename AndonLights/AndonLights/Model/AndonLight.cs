using AndonLights.DTOs;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Collections;

namespace AndonLights.Model;

public class AndonLight
{
    public int Id { get; set; }
    public LightStates CurrentState { get; set; }

    public string Name { get; set; }

    public required DateTime DateOfCreation { get; init; }

    public List<State> States { get;set; }

    

    [SetsRequiredMembers]
    public AndonLight(string Name)
    {
        this.Name = Name;
        CurrentState = LightStates.Green;
        DateOfCreation = DateTime.Now;
        States = new List<State>
        {
            new State(LightStates.Green),
            new State(LightStates.Yellow),
            new State(LightStates.Red),
        };

    }

    public void SwitchedState(AndonLightDTO andonLight)
    {
        switch (this.CurrentState)
        {
            case LightStates.Green:
                getStateWithColour(LightStates.Green).closeState(andonLight.time);
                break;
            case LightStates.Yellow:
                getStateWithColour(LightStates.Yellow).closeState(andonLight.time);
                break;
            case LightStates.Red:
                getStateWithColour(LightStates.Red).closeState(andonLight.time);
                break;
        }
        switch(andonLight.State)
        {
            case LightStates.Green:
                getStateWithColour(LightStates.Green).activateState(andonLight.time);
                CurrentState = LightStates.Green;
                break;
            case LightStates.Yellow:
                getStateWithColour(LightStates.Yellow).activateState(andonLight.time);
                CurrentState = LightStates.Yellow;
                break;
            case LightStates.Red:
                getStateWithColour(LightStates.Red).activateState(andonLight.time);
                CurrentState = LightStates.Red;
                break;
        }
    }

    public StatsResponseDTO GetDailyStatsFromStates(StatsQuestionDTO questionDTO)
    {
        return new StatsResponseDTO(States[0].GetDailyStats(questionDTO.Time)
            , States[1].GetDailyStats(questionDTO.Time)
            , States[2].GetDailyStats(questionDTO.Time));
    }
    public StatsResponseDTO GetMonthlyStatsFromStates(StatsQuestionDTO questionDTO)
    {
        return new StatsResponseDTO(States[0].GetMonthlyStats(questionDTO.Time)
            , States[1].GetMonthlyStats(questionDTO.Time)
            , States[2].GetMonthlyStats(questionDTO.Time));
    }
    private State getStateWithColour(LightStates lightStates) 
    {
        if(lightStates == LightStates.Green)
        {
            return States[0];
        }
        if(lightStates == LightStates.Yellow)
        { 
            return States[1];
        }
        return States[2];
    }

}
