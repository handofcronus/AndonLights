using NodaTime;

namespace AndonLights.Model;

public class Client
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ApiKey { get; set; }
    public string? NewApiKey { get; set; }

    public Boolean NewKeyRequested { get; set; } = false;
    public ZonedDateTime OldKeyDeadline { get; set;}

    public Client(string name , string apikey)
    {
        Name = name;
        ApiKey = apikey;
    }
    public Client(int id, string name, string apiKey, string? newApiKey, bool newKeyRequested, ZonedDateTime oldKeyDeadline)
    {
        Id = id;
        Name = name;
        ApiKey = apiKey;
        NewApiKey = newApiKey;
        NewKeyRequested = newKeyRequested;
        OldKeyDeadline = oldKeyDeadline;
    }
}
