namespace RuomRaCoffe.Shared.Dtos;

public  record ApiResult (bool Success, string? Error)
{
    public static ApiResult ApiSuccess() => new(true, null);
    public static ApiResult ApiError(string ErrorMessage) => new(false, ErrorMessage);

}

public record ApiResult<TData>(bool Success,TData Data ,string? Error)
{
    public static ApiResult<TData> ApiSuccess( TData data) => new(true,data ,null);
    public static ApiResult<TData> ApiError(string ErrorMessage) => new(false,default! ,ErrorMessage);

}