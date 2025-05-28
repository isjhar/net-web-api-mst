using NewWebApiTemplate.Application.Constants;
using NewWebApiTemplate.Domain.Enums;

namespace NetWebApiTemplate.Api.Extensions
{
    public static class AuthorizationPolicyExtensions
    {
        public static IServiceCollection AddCustomAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorizationBuilder()
                .AddPolicy(PermissionKey.ViewUser, policy => policy.RequireClaim(AppConstant.Permission.ToLower(), PermissionKey.ViewUser));

            return services;
        }
    }
}
