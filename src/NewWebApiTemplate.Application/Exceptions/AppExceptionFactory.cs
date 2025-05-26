namespace NewWebApiTemplate.Application.Exceptions
{
    public static class AppExceptionFactory
    {
        public static readonly AppException JwtIsNotDefined = new("Configuration Jwt is not defined");

        public static readonly AppException InvalidParams = new("Invalid params");

        public static readonly AppException Forbidden = new("Forbidden");

        public static readonly AppException EntityNotFound = new("Entity is not found");
    }
}
