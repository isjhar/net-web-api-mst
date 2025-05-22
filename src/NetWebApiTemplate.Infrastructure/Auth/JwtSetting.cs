namespace NetWebApiTemplate.Infrastructure.Auth
{
    public class JwtSetting
    {
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public required string Secret { get; set; }
        public required int RefreshTokenExpiryDays { get; set; }
    }
}
