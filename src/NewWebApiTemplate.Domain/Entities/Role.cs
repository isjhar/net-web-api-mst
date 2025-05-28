namespace NewWebApiTemplate.Domain.Entities
{
    public class Role
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public IList<string> Permissions { get; set; } = new List<string>();
    }
}
