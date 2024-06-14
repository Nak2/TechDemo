using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Interface.ApiService;

internal class ApiService : IApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7259/"),
            // Add api key to the header.
            DefaultRequestHeaders =
            {
                { "x-api-key", "3543593e-290a-423b-812f-335beceea0d0" }
            }
        };
    }

    /// <summary>
    /// Get data from the server.
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public async Task<ApiResult> GetDataAsync(string url)
    {
        var response = await _httpClient.GetAsync(url);
        var result = new ApiResult((int)response.StatusCode);
        if (response.IsSuccessStatusCode)
        {
            result.Content = await response.Content.ReadAsStringAsync();
        };

        return result;
    }

    /// <summary>
    /// Get data from the server.
    /// </summary>
    /// <param name="url"></param>
    /// <param name="bodyData"></param>
    /// <returns></returns>
    public async Task<ApiResult<T>> GetDataAsync<T>(string url)
    {
        var response = await _httpClient.GetAsync(url);
        var result = new ApiResult<T>((int)response.StatusCode);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            result.Content = JsonSerializer.Deserialize<T>(content);
        }
        return result;
    }

    /// <summary>
    /// Post data to the server.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="bodyData"></param>
    /// <returns></returns>
    public async Task<ApiResult<T>> PostDataAsync<T>(string url, string? bodyData)
    {
        var response = await _httpClient.PostAsync(url, new StringContent(bodyData ?? string.Empty, Encoding.UTF8, "application/json"));
        var result = new ApiResult<T>((int)response.StatusCode);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            result.Content = JsonSerializer.Deserialize<T>(content);
        }
        return result;
    }

    public async Task<ApiResult<T>> PutDataAsync<T>(string url, string? bodyData)
    {
        var response = await _httpClient.PutAsync(url, new StringContent(bodyData ?? string.Empty, Encoding.UTF8, "application/json"));
        var result = new ApiResult<T>((int)response.StatusCode);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            result.Content = JsonSerializer.Deserialize<T>(content);
        }
        return result;
    }

    public async Task<ApiResult<T>> DeleteDataAsync<T>(string url)
    {
        var response = await _httpClient.DeleteAsync(url);
        var result = new ApiResult<T>((int)response.StatusCode);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            result.Content = JsonSerializer.Deserialize<T>(content);
        }
        return result;
    }
}
