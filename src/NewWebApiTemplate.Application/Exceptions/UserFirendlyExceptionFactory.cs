namespace NewWebApiTemplate.Application.Exceptions
{
    public static class UserFirendlyExceptionFactory
    {
        public static readonly UserFriendlyException UsernameOrPasswordIncorrect = new("Username or password was incorrect");

        public static readonly UserFriendlyException InvalidParams = new("Invalid params");
    }
}
