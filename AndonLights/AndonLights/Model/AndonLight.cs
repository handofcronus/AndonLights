using AndonLights.DTOs;
using NodaTime;
using System.Diagnostics.CodeAnalysis;

namespace AndonLights.Model;

public class AndonLight
{
    public int Id { get; set; }
    public LightStates CurrentState { get; set; }

    public string Name { get; set; }

    public required ZonedDateTime DateOfCreation { get; init; }

    public List<State> States { get;set; }

    

    [SetsRequiredMembers]
    public AndonLight(string Name)
    {
        this.Name = Name;
        CurrentState = LightStates.Green;
        DateOfCreation =new ZonedDateTime( SystemClock.Instance.GetCurrentInstant(),DateTimeZone.Utc);
        States = new List<State>
        {
            new State(LightStates.Green),
            new State(LightStates.Yellow),
            new State(LightStates.Red),
        };
    }
    protected AndonLight() 
    {
        States = new List<State>();
        Name = "Default";
    }

    public string GetLastErrorMessage()
    {
        State currentState = GetStateWithColour(CurrentState);
        return currentState.GetCurrentSession().ErrorMessage ?? "";
    }
    public void SwitchedState(LightStates newState,string errorMessage)
    {
        try
        {
            switch (this.CurrentState)
            {
                case LightStates.Green:
                    GetStateWithColour(LightStates.Green).CloseState();
                    break;
                case LightStates.Yellow:
                    GetStateWithColour(LightStates.Yellow).CloseState();
                    break;
                case LightStates.Red:
                    GetStateWithColour(LightStates.Red).CloseState();
                    break;
            }
            switch (newState)
            {
                case LightStates.Green:
                    GetStateWithColour(LightStates.Green).ActivateState(errorMessage);
                    CurrentState = LightStates.Green;
                    break;
                case LightStates.Yellow:
                    GetStateWithColour(LightStates.Yellow).ActivateState(errorMessage);
                    CurrentState = LightStates.Yellow;
                    break;
                case LightStates.Red:
                    GetStateWithColour(LightStates.Red).ActivateState(errorMessage);
                    CurrentState = LightStates.Red;
                    break;
            }
        }
        catch(InvalidOperationException)
        {
            throw new InvalidOperationException();
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
    private State GetStateWithColour(LightStates lightState) 
    {
        foreach(var state in States)
        {
            if(state.StateColour==lightState)
            {
                return state;
            }
        }
        throw new InvalidOperationException();

    }

}
