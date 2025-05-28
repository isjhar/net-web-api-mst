using Microsoft.AspNetCore.Identity;

namespace NewWebApiTemplate.Persistence.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public required string Name { get; set; }
    }
}
