using AndonLights.Services.Interfaces;
using System.Security.Cryptography;

namespace AndonLights.Services;

public class ApiKeyService : IApiKeyService
{
    private const string _prefix = "AL-";
    private const int _numberOfSecureBytesToGenerate = 32;
    private const int _lengthOfKey = 32;

    public string GenerateApiKey()
    {
        var bytes = RandomNumberGenerator.GetBytes(_numberOfSecureBytesToGenerate);
        string base64String = Convert.ToBase64String(bytes)
            .Replace("+", "-")
            .Replace("/", "_");
        var keyLenght = _lengthOfKey - _prefix.Length;
        base64String = base64String[..keyLenght];
        return _prefix + base64String;
    }
}
