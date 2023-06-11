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
        public static string ToStrfing(LightStates states)
        {
            return "";
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
