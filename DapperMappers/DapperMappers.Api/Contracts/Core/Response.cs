namespace DapperMappers.Api.Contracts.Core;

public abstract record Response<T> : BaseResponse
{
    public T Result { get; set; }

    protected Response(T result, int statusCode)
    {
        Result = result;
        StatusCode = statusCode;
    }
}