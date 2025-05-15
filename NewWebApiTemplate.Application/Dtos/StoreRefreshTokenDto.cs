namespace NewWebApiTemplate.Application.Dtos
{
    public class StoreRefreshTokenDto
    {
        public Guid UserId { get; set; }

        public required string Token { get; set; }
    }
}
