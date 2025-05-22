using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using NetWebApiTemplate.Api.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NetWebApiTemplate.Api.OperationFilters
{
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAuthorize = false;

            var declaringType = context.MethodInfo.DeclaringType;

            if (declaringType != null)
            {
                var declaringTypeAttributes = declaringType.GetCustomAttributes(true);
                var methodInfoAttributes = context.MethodInfo.GetCustomAttributes(true);

                hasAuthorize = HasAttribute<AuthorizeAttribute>(declaringTypeAttributes, methodInfoAttributes) ||
                    HasAttribute<PermissionAttribute>(declaringTypeAttributes, methodInfoAttributes);
            }

            var allowAnonymous = context.MethodInfo.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any();

            if (hasAuthorize && !allowAnonymous)
            {
                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        [ new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            }
                        ] = new List<string>()
                    }
                };
            }
        }

        private bool HasAttribute<T>(object[] declaringTypeAttributes, object[] methodInfoAttributes)
        {
            return declaringTypeAttributes.OfType<T>().Any() ||
                    methodInfoAttributes.OfType<T>().Any();
        }
    }
}
