namespace AndonLights.Model
{
    public enum LightStates
    {
        Green,
        Yellow,
        Red
    }
    
    public static class LightStateHelper
    {
        public static string ToString(LightStates states)
        {
            switch (states)
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

        public static LightStates FromString(string value) 
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
    
}
