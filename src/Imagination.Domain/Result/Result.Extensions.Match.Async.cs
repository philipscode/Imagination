namespace Imagination.Domain.Result;

public static partial class ResultExtensions
    {
        public static async Task<TResult> Match<TValue, TError, TResult>(
            this Task<Result<TValue, TError>> resultTask,
            Func<TValue, TResult> successFunc,
            Func<TError, TResult> failureFunc)
        {
            var result = await resultTask;

            return result.IsSuccess
                ? successFunc(result.Value)
                : failureFunc(result.Error);
        }
    }
