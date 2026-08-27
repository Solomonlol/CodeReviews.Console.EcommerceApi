namespace Solomonlol.EcommerseApi.MyResults

{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string? Error { get; }
        protected Result(bool isSuccess, string? error = null) 
        {
            if (isSuccess && error != null)
                throw new InvalidOperationException("Successful result cannot have an error!");
            if (!isSuccess && error == null)
                throw new InvalidOperationException("Failed result must have an error!");

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new(true);
        public static Result Failure(string error) => new(false, error);

        public static Result<T> Success<T>(T value)=>Result<T>.Success(value);
        public static Result<T> Failure<T>(string error) => Result<T>.Failure(error);
    }
    public class Result<T> : Result
    {
        public T? Value { get; }
        private Result(T? value, bool success, string? error = null) : base(success, error)
        {
            Value = value;
        }
        public static Result<T> Success(T value) => new(value, true);
        public static Result<T> Failure(string error) => new(default, false, error);
    }
}

