using System.Diagnostics;
using Ardalis.GuardClauses;

namespace Imagination.Domain.Result;

[DebuggerDisplay("{Value}")]
public readonly struct FailureReason
{
    private readonly string _value;
    private readonly bool _hasValue;

    public string Value
        => _hasValue
            ? _value
            : throw new InvalidOperationException("Error text is not initialized");

    public static FailureReason From(string value) => new(value);

    private FailureReason(string value)
    {
        Guard.Against.NullOrEmpty(value, nameof(value));

        _value = value;
        _hasValue = true;
    }
}
