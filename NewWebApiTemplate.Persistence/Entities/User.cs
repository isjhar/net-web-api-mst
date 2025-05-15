using System.ComponentModel.DataAnnotations;

namespace NewWebApiTemplate.Persistence.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        public required string Username { get; set; }

        public required string Password { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }

        public List<Role> Roles { get; set; } = new List<Role>();
    }
}
