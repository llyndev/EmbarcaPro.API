namespace EmbarcaPro.API.Common.Results
{
    public sealed record ServiceResult<T>(
        bool Success,
        string Message,
        T? Data,
        ErrorType ErrorType = ErrorType.None)
    {
        public static ServiceResult<T> Ok(T data, string message)
            => new(true, message, data);

        public static ServiceResult<T> Fail(string message, ErrorType errorType)
            => new(false, message, default, errorType);

    }
}
