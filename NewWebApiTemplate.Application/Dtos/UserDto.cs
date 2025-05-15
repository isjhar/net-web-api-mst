namespace NewWebApiTemplate.Application.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public IList<RoleDto> Roles { get; set; } = new List<RoleDto>();
    }
}
