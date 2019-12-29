namespace DapperMappers.Api.Contracts
{
    public abstract class Response<T> : BaseResponse
    {
        public T Result { get; set; }

        public Response(T result, int statusCode)
        {
            Result = result;
            StatusCode = statusCode;
        }
    }
}
