using System.ComponentModel.DataAnnotations;

namespace NewWebApiTemplate.Persistence.Entities
{
    public class Permission
    {
        [Key]
        public required string Key { get; set; }

        public required string Description { get; set; }
    }
}
