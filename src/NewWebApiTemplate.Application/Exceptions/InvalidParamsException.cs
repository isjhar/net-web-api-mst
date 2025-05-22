namespace NewWebApiTemplate.Application.Exceptions
{
    public class InvalidParamsException : AppException
    {
        public InvalidParamsException() : base("Invalid params") { }
    }
}
