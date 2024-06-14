namespace Interface.ApiService;

public class ApiResult(int statusCode)
{
    public int StatusCode { get; set; } = statusCode;
    public string? Content { get; set; }
}

public class ApiResult<T>(int statusCode)
{
    public int StatusCode { get; set; } = statusCode;
    public T? Content { get; set; }
}
