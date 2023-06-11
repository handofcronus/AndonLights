using AndonLights.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AndonLights.Controllers.Attributes;

public class ApiKeyAttribute : Attribute, IAuthorizationFilter
{
    private readonly string  ApiKeyHeaderName = "Andon-Api-Key";
    private readonly IClientService _clientService; 

    public ApiKeyAttribute(IClientService clientService)
    {
        _clientService = clientService;
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if(!IsApiKeyValid(context.HttpContext))
        {
            context.Result = new UnauthorizedResult();
        }
    }

    private bool IsApiKeyValid(HttpContext context)
    {
        string? apiKey = context.Request.Headers[ApiKeyHeaderName];

        return _clientService.CheckApiKey(apiKey);
    }
}
