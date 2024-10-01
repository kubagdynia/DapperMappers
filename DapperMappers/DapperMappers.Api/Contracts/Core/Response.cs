namespace DapperMappers.Api.Contracts.Core
{
    public abstract class Response<T> : BaseResponse
    {
        public T Result { get; set; }

        protected Response(T result, int statusCode)
        {
            Result = result;
            StatusCode = statusCode;
        }
    }
}
