using Ecommerce.Common.Enum;

namespace Ecommerce.Common.Results
{
    public class Result
    {
        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;
        public string? Error { get; }
        public ErrorType ErrorType { get; }
        public string? Message { get; }
        protected Result (bool isSuccess, string? error, ErrorType errorType, string? message) 
        {
                IsSuccess = isSuccess;
                Error = error;
                ErrorType = errorType;
                Message = message;
        }

         public static Result Success(string? message = null)
        {
            return new(true, null, ErrorType.None, message);
        }

        public static Result Failure(string error, ErrorType errorType = ErrorType.Validation)
        {
            return new(false, error, errorType, null);
        }
             
    }

    public class Result<T>: Result
    {
        public T? Value { get; }
        private Result(
        T? value,
        bool isSuccess,
        string? message,
        string? error,
        ErrorType errorType)
        : base(isSuccess, error, errorType, message)
        {
            Value = value;
        }
        public static Result<T> Success(
         T value,
         string? message = null)
        {
            return new(
                value,
                true,
                message,
                null,
                ErrorType.None);
        }
        public new static Result<T> Failure(
        string error,
        ErrorType errorType = ErrorType.Validation)
        {
            return new(
                default,
                false,
                null,
                error,
                errorType);
        }
    }
}
