namespace NewWebApiTemplate.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public required string Username { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }

        public IList<Role> Roles { get; set; } = new List<Role>();
    }
}
