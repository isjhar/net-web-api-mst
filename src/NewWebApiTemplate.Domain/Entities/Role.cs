using NewWebApiTemplate.Domain.Enums;

namespace NewWebApiTemplate.Domain.Entities
{
    public class Role
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public IList<PermissionKey> Permissions { get; set; } = new List<PermissionKey>();
    }
}
