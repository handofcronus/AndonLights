using AndonLights.DAL;
using AndonLights.Model;
using AndonLights.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using NodaTime;

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
        var check = _dbContext.Clients.Where(c => c.Name == name).SingleOrDefault();
        if(check!=null)
        {
            throw new InvalidOperationException("Username taken.");
        }
        var client = new Client(name, _apiKeyService.GenerateApiKey());
        _dbContext.Clients.Add(client);
        _dbContext.SaveChanges();
        return client;
    }


    public bool CheckApiKey(string ApiKey) 
    {
        var client = _dbContext.Clients.Where(c => c.ApiKey == ApiKey || c.NewApiKey == ApiKey).SingleOrDefault();
        if (client!=null &&client.NewKeyRequested)
        {
            var now = new ZonedDateTime(SystemClock.Instance.GetCurrentInstant(), DateTimeZone.Utc);
            if (client.OldKeyDeadline.ToDateTimeUtc() < now.ToDateTimeUtc())
            {
                if (client.NewApiKey == ApiKey)
                {
                    client.ApiKey = client.NewApiKey;
                    client.NewApiKey = string.Empty;
                    client.NewKeyRequested = false;
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    client.ApiKey = client.NewApiKey;
                    client.NewApiKey = string.Empty;
                    client.NewKeyRequested = false;
                    _dbContext.SaveChanges();
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        return client != null;


    }

    public Client RequestNewKey(string name)
    {
        var client = _dbContext.Clients.Where(c => c.Name == name).Single();
        if(client.NewKeyRequested)
        {
            throw new InvalidOperationException("Already requested new key, wait 12 hours.");
        }
        else
        {
            var dealine = DateTime.Now.AddHours(12);
            client.NewKeyRequested = true;
            client.OldKeyDeadline = new ZonedDateTime(new LocalDateTime(dealine.Year, dealine.Month, dealine.Day, dealine.Hour, dealine.Minute), DateTimeZone.Utc, Offset.Zero);
            client.NewApiKey = _apiKeyService.GenerateApiKey();
            _dbContext.SaveChanges();
            return client;
        }
        
    }
}
