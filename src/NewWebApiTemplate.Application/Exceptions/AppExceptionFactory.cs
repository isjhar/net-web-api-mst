namespace NewWebApiTemplate.Application.Exceptions
{
    public static class AppExceptionFactory
    {
        public static readonly AppException JwtIsNotDefined = new("Configuration Jwt is not defined");

        public static readonly AppException UsernameOrPasswordIncorrect = new("Username or password was incorrect");

        public static readonly AppException InvalidParams = new("Invalid params");

        public static readonly AppException Forbidden = new("Forbidden");
    }
}
