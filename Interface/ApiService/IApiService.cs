
namespace Interface.ApiService;

public interface IApiService
{
    /// <summary>
    /// Delete data from the server.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <returns></returns>
    Task<ApiResult<T>> DeleteDataAsync<T>(string url);

    /// <summary>
    /// Get data from the server.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <returns></returns>
    Task<ApiResult<T>> GetDataAsync<T>(string url);

    /// <summary>
    /// Get data from the server.
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    Task<ApiResult> GetDataAsync(string url);

    /// <summary>
    /// Post data to the server.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="bodyData"></param>
    /// <returns></returns>
    Task<ApiResult<T>> PostDataAsync<T>(string url, string? bodyData);

    /// <summary>
    /// Put data to the server.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="url"></param>
    /// <param name="bodyData"></param>
    /// <returns></returns>
    Task<ApiResult<T>> PutDataAsync<T>(string url, string? bodyData);
}