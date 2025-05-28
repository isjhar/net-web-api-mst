namespace NewWebApiTemplate.Persistence.Entities
{
    public class RefreshToken
    {
        public long Id { get; set; }

        public Guid UserId { get; set; }

        public ApplicationUser? User { get; set; }

        public required string Token { get; set; }

        public required DateTime ExpiresAt { get; set; }
    }
}
