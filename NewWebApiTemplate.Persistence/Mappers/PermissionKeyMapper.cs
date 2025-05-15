using NewWebApiTemplate.Domain.Enums;

namespace NewWebApiTemplate.Persistence.Mappers
{
    public static class PermissionKeyMapper
    {
        public static PermissionKey ToDomain(string key)
        {
            switch (key)
            {
                case "create.user":
                    return PermissionKey.CreateUser;
                case "edit.user":
                    return PermissionKey.EditUser;
                case "view.user":
                    return PermissionKey.ViewUser;
                case "delete.user":
                    return PermissionKey.EditUser;
                case "create.role":
                    return PermissionKey.CreateRole;
                case "edit.role":
                    return PermissionKey.EditRole;
                case "view.role":
                    return PermissionKey.ViewRole;
                case "delete.role":
                    return PermissionKey.EditRole;
                default:
                    return PermissionKey.None;
            }
        }
    }
}
