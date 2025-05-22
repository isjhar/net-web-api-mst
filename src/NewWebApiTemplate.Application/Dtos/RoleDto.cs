using NewWebApiTemplate.Domain.Enums;

namespace NewWebApiTemplate.Application.Dtos
{
    public class RoleDto
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public IList<PermissionKey> Permissions { get; set; } = new List<PermissionKey>();
    }
}
