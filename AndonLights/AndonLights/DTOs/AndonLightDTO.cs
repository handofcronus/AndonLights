using AndonLights.Model;

namespace AndonLights.DTOs;

public class AndonLightDTO
{
    public int ID { get; set; }
    public string Name { get; set; }
    public LightStates State { get; set; }
    public string? ErrorMessage { get; set; }

    public DateTime time { get; set; } = DateTime.Now;

    public AndonLightDTO(string name)
    {
        Name = name;
    }
}
