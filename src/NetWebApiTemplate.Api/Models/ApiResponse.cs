namespace NetWebApiTemplate.Api.Models
{
    public class ApiResponse<T>
    {
        public string? Message { get; set; }
        public T? Data { get; set; }

        public ApiResponse(T data, string? message = null)
        {
            Message = message;
            Data = data;
        }

        public ApiResponse(string errorMessage)
        {
            Message = errorMessage;
        }
    }
}
