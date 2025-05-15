using System.ComponentModel.DataAnnotations;

namespace NewWebApiTemplate.Persistence.Entities
{
    public class Role
    {
        [Key]
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public List<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
