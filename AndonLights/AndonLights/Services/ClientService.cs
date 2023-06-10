using AndonLights.DAL;
using AndonLights.Model;
using AndonLights.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AndonLights.Services;

public class ClientService : IClientService
{
    private readonly AndonLightsDbContext _dbContext;
    private readonly IApiKeyService _apiKeyService;
    public ClientService(AndonLightsDbContext dbContext, IApiKeyService apiKeyService)
    {
        _dbContext = dbContext;
        _apiKeyService = apiKeyService;
    }


    public Client CreateClient(string name)
    {
        var client = new Client
        {
            Name = name,
            ApiKey = _apiKeyService.GenerateApiKey(),
        };
        _dbContext.Clients.Add(client);
        _dbContext.SaveChanges();
        return client;
    }

    public bool CheckApiKey(string ApiKey) 
    {
        var client = _dbContext.Clients.Where(c => c.ApiKey == ApiKey).SingleOrDefault();
        return client != null;
    }


}
