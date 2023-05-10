using AndonLights.Model;

namespace AndonLights.DTOs;

public class AndonLightDTO
{
    public int ID { get; set; }
    public string Name { get; set; }
    public LightStates StateCode { get; set; }
    public string? State { get; set; }
    public string? ErrorMessage { get; set; }

    public DateTime DateofCreation { get; set; } 

    public AndonLightDTO(string name)
    {
        Name = name;
    }
}

public record AndonStateDTO(int id,string state)
{
    public string ErrorMessage { get; set; }
}
