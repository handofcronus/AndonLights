using AndonLights.DTOs;
using System.Diagnostics.CodeAnalysis;

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
    protected AndonLight() 
    {
        States = new List<State>();
    }

    public void SwitchedState(AndonLightDTO andonLight)
    {
        try
        {
            switch (this.CurrentState)
            {
                case LightStates.Green:
                    GetStateWithColour(LightStates.Green).CloseState(andonLight.time);
                    break;
                case LightStates.Yellow:
                    GetStateWithColour(LightStates.Yellow).CloseState(andonLight.time);
                    break;
                case LightStates.Red:
                    GetStateWithColour(LightStates.Red).CloseState(andonLight.time);
                    break;
            }
            switch (andonLight.State)
            {
                case LightStates.Green:
                    GetStateWithColour(LightStates.Green).ActivateState(andonLight.time);
                    CurrentState = LightStates.Green;
                    break;
                case LightStates.Yellow:
                    GetStateWithColour(LightStates.Yellow).ActivateState(andonLight.time);
                    CurrentState = LightStates.Yellow;
                    break;
                case LightStates.Red:
                    GetStateWithColour(LightStates.Red).ActivateState(andonLight.time);
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
