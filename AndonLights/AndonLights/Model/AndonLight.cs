using AndonLights.DTOs;
using System.Diagnostics.CodeAnalysis;

namespace AndonLights.Model;

public class AndonLight
{
    public int Id { get; set; }
    public LightStates CurrentState { get; set; }

    public string Name { get; set; }

    public required DateTime DateOfCreation { get; init; }

    private State _greenState;
    private State _yellowState;
    private State _redState;
    private State _blueState;

    [SetsRequiredMembers]
    public AndonLight(string Name)
    {
        this.Name = Name;
        CurrentState = LightStates.Green;
        DateOfCreation = DateTime.Now;
        _greenState = new State();
        _yellowState = new State();
        _redState = new State();
        _blueState = new State();
    }

    public void SwitchedState(AndonLightDTO andonLight)
    {
        switch (this.CurrentState)
        {
            case LightStates.Green:
                _greenState.closeState(andonLight.time);
                break;
            case LightStates.Yellow:
                _yellowState.closeState(andonLight.time);
                break;
            case LightStates.Red:
                _redState.closeState(andonLight.time);
                break;
            case LightStates.Blue:
                _blueState.closeState(andonLight.time);
                break;
        }
        switch(andonLight.State)
        {
            case LightStates.Green:
                _greenState.activateState(andonLight.time);
                break;
            case LightStates.Yellow:
                _yellowState.activateState(andonLight.time);
                break;
            case LightStates.Red:
                _redState.activateState(andonLight.time);
                break;
            case LightStates.Blue:
                _blueState.activateState(andonLight.time);
                break;
        }
    }

}
