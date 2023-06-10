using AndonLights.Model;

namespace AndonLights.Services.Interfaces;

public interface IClientService
{
    public Client CreateClient(string name);
    public bool CheckApiKey(string ApiKey);
}
