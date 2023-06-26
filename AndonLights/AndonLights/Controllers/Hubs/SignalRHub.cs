using AndonLights.Controllers.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace AndonLights.Controllers.Hubs;

public class SignalRHub : Hub<IHubClient>
{
    public async Task BroadcastMessage()
    {
        
    }
}
