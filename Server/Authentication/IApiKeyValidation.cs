namespace Server.Authentication;

public interface IApiKeyValidation
{
    bool Validate(string apiKey);
}