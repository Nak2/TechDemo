namespace Server.Authentication;

public class ApiKeyValidation(IConfiguration configuration) : IApiKeyValidation
{
    private readonly IConfiguration _configuration = configuration;

    public bool Validate(string apiKey)
    {
        if (string.IsNullOrEmpty(apiKey))
        {
            return false;
        }

        var validApiKey = _configuration.GetValue<string>(Constants.ApiKeyName);
        return apiKey == validApiKey;
    }
}
