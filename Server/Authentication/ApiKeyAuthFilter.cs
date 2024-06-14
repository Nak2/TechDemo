using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Server.Authentication;

public class ApiKeyAuthFilter(IApiKeyValidation apiKeyValidation) : IAuthorizationFilter
{
    private readonly IApiKeyValidation _apiKeyValidation = apiKeyValidation;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userApiKey = context.HttpContext.Request.Headers[Constants.ApiKeyName];
        if(string.IsNullOrEmpty(userApiKey))
        {
            context.Result = new BadRequestResult();
            return;
        }

        if (!_apiKeyValidation.Validate(userApiKey!))
        {
            context.Result = new UnauthorizedResult();
        }
    }
}
