namespace Template.Domain.Entities.ResponseEntity;

public class Result
{
    public bool SuccessStatus { get; init; }
    public List<string>? Errors { get; set; }
    public Result(bool success, List<string>? errors)
    {
        SuccessStatus = success;
        Errors = (errors == null ? new List<string>() : errors);
    }
    public static Result Success()
    {
        return new Result(true, new List<string>());
    }
    public static Result<T> Success<T>()
    {
        return new Result<T>(default(T), true, new List<string>());
    }
    public static Result<T> Success<T>(T data)
    {
        return new Result<T>(data, true, new List<string>());
    }
    public static Result Failure(List<string> errors)
    {
        return new Result(false, errors);
    }
    public static Result<T> Failure<T>(List<string> errors)
    {
        return new Result<T>(default, false, errors);
    }

}
public class Result<T> : Result
{
    public T? Data { get; }
    internal Result(T? data, bool success, List<string>? errors)
         : base(success, errors)
    {
        Data = data;
        this.Errors = errors;
    }
    public static Result<T> Success(T data)
    {
        return new Result<T>(data, true, new List<string>());
    }

    public static Result<T> Failure(List<string> errors)
    {
        return new Result<T>(default, false, errors);
    }
}
