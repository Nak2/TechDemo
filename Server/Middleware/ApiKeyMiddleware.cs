using Server.Authentication;
using System.Net;

namespace Server.Middleware;

public class ApiKeyMiddleware(RequestDelegate next, IApiKeyValidation apiKeyValidation)
{
    private readonly RequestDelegate _next = next;
    private readonly IApiKeyValidation _apiKeyValidation = apiKeyValidation;

    public async Task InvokeAsync(HttpContext context)
    {
        var userApiKey = context.Request.Headers[Constants.ApiKeyHeaderName];
        if(string.IsNullOrEmpty(userApiKey))
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return;
        }

        if (!_apiKeyValidation.Validate(userApiKey!))
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return;
        }

        await _next(context);
    }
}
