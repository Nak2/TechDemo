using System.Diagnostics.CodeAnalysis;

namespace Server.Database;

/// <summary>
/// Represents the result of an operation.
/// </summary>
/// <param name="success"></param>
public class OperationResult(bool success)
{
    public bool Success { get; set; } = success;
    public string Error { get; set; } = string.Empty;
}

/// <summary>
/// Represents the result of an operation with a result.
/// </summary>
/// <typeparam name="T">The result type.</typeparam>
/// <param name="success">The success of the operation.</param>
/// <param name="result">The result of the operation.</param>
public class OperationResult<T>(bool success, T? result = default) : OperationResult(success)
{
    [MemberNotNullWhen(true, nameof(Result))]
    public new bool Success
    {
        get => base.Success;
        set => base.Success = value;
    }

    public T? Result { get; set; } = result;
}