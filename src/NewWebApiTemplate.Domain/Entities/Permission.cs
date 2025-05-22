using NewWebApiTemplate.Domain.Enums;

namespace NewWebApiTemplate.Domain.Entities
{
    public class Permission
    {
        public PermissionKey Key { get; set; }

        public required string Descrption { get; set; }
    }
}
