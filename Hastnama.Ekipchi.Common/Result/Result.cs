using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Common.Result
{
    public class Result<T>
    {
        public T Data { get; private set; }
        private string Message { get; set; }

        public ObjectResult ApiResult { get; set; }
        public bool Success { get; set; }

        public static Result<T> SuccessFull(T data)
        {
            return new Result<T> {ApiResult = new OkObjectResult(data), Data = data, Message = null, Success = true};
        }

        public static Result<T> Failed(ObjectResult error)
        {
            return new Result<T> {ApiResult = error, Success = false, Message = error.Value?.ToString()};
        }
    }

    public class Result
    {
        private string Message { get; set; }

        public ObjectResult ApiResult { get; set; }
        public bool Success { get; set; }

        public static Result SuccessFull()
        {
            return new Result {Message = null, Success = true};
        }

        public static Result Failed(ObjectResult error)
        {
            return new Result {ApiResult = error, Success = false, Message = error.Value?.ToString()};
        }
    }
}