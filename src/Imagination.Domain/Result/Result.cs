namespace Imagination.Domain.Result;

public sealed class Result<TValue, TError>
{
    private readonly TValue? _value;
    private readonly TError? _error;

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public TValue Value
        => IsSuccess
            ? _value!
            : throw new InvalidOperationException("Success value is not defined");

    public TError Error
        => IsFailure
            ? _error!
            : throw new InvalidOperationException("Failure is not defined");

    private Result(TValue value)
    {
        _value = value;
        IsSuccess = true;
    }

    private Result(TError error)
    {
        _error = error;
        IsSuccess = false;
    }

    public static Result<TValue, TError> FromSuccess(TValue value) => new(value);

    public static Result<TValue, TError> FromFailure(TError error) => new(error);

    public static implicit operator Result<TValue, TError>(TValue value) => FromSuccess(value);

    public static implicit operator Result<TValue, TError>(TError error) => FromFailure(error);
}
