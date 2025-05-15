namespace NewWebApiTemplate.Application.Dtos
{
    public class PairTokenDto
    {
        public required string AccessToken { get; set; }

        public required string RefreshToken { get; set; }
    }
}
