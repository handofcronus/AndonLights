namespace AndonLights.Controllers.Interfaces;

public interface IHubClient
{
    Task BroadcastMessage();

}
