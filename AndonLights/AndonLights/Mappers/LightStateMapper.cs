using AndonLights.Model;
using System.Runtime.CompilerServices;

namespace AndonLights.Mappers;

public static class LightStateMapper
{
    public static string toString(this LightStates lightStates)
    {
        switch (lightStates)
        {
            case LightStates.Red:
                return "Red";
            case LightStates.Green:
                return "Green";
            case LightStates.Yellow:
                return "Yellow";
            default:
                throw new InvalidOperationException();
        }
    }
    public static LightStates toLightState(this string value)
    {
        switch (value.ToUpper())
        {
            case "RED":
                return LightStates.Red;
            case "YELLOW":
                return LightStates.Yellow;
            case "GREEN":
                return LightStates.Green;
            default:
                throw new InvalidOperationException();
        }
    }
}
