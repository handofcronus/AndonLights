using AndonLights.DTOs;
using NodaTime;
using NodaTime.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace AndonLights.Model;

public class AndonLight
{
    public int Id { get; set; }
    public LightStates CurrentState { get; set; }

    public string Name { get; set; }

    public required ZonedDateTime DateOfCreation { get; init; }

    public List<State> States { get;set; }
    public string LastErrorMessage { get; set; }

    

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
        LastErrorMessage = string.Empty;
    }
    protected AndonLight() 
    {
        States = new List<State>();
        Name = "Default";
        LastErrorMessage = string.Empty;
    }

    public string GetLastErrorMessage()
    {
       
        return LastErrorMessage;
    }
    public void SwitchedState(LightStates newState,string errorMessage)
    {
        LastErrorMessage = errorMessage;
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
                    GetStateWithColour(LightStates.Green).ActivateState();
                    CurrentState = LightStates.Green;
                    break;
                case LightStates.Yellow:
                    GetStateWithColour(LightStates.Yellow).ActivateState();
                    CurrentState = LightStates.Yellow;
                    break;
                case LightStates.Red:
                    GetStateWithColour(LightStates.Red).ActivateState();
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
        return new StatsResponseDTO(States[0].GetDailyStats(questionDTO.Date.ToLocalDateTime())
            , States[1].GetDailyStats(questionDTO.Date.ToLocalDateTime())
            , States[2].GetDailyStats(questionDTO.Date.ToLocalDateTime()));
    }
    public StatsResponseDTO GetMonthlyStatsFromStates(StatsQuestionDTO questionDTO)
    {
        return new StatsResponseDTO(States[0].GetMonthlyStats(questionDTO.Date.ToLocalDateTime())
            , States[1].GetMonthlyStats(questionDTO.Date.ToLocalDateTime())
            , States[2].GetMonthlyStats(questionDTO.Date.ToLocalDateTime()));
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
