using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NetWebApiTemplate.Api.Models;
using NewWebApiTemplate.Domain.Enums;
using System.Net;

namespace NetWebApiTemplate.Api.Attributes
{
    public class PermissionAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly PermissionKey _permission;

        public PermissionAttribute(PermissionKey permission)
        {
            _permission = permission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user == null ||
                user.Identity == null ||
                !user.Identity.IsAuthenticated ||
                !user.HasClaim(c => c.Type == "Permission" && Enum.Parse<PermissionKey>(c.Value) == _permission))
            {
                context.Result = new JsonResult(new ApiResponse<string>("Forbidden")) { StatusCode = (int)HttpStatusCode.Forbidden };
            }
        }
    }
}
