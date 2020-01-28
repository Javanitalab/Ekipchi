using Hastnama.Ekipchi.Common.Enum;
using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Common.Result
{
    public class Result<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }

        public IActionResult Error { get; set; }
        public bool Success { get; set; }

        public static Result<T> SuccessFull(T data)
        {
            return new Result<T> {Data = data, Message = null, Success = true};
        }
        
        public static Result<T> Failed(IActionResult error)
        {
            return new Result<T> {Error = error, Success = false};
        }
    }
}