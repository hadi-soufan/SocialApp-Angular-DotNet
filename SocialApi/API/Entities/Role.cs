namespace API.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public required string Description { get; set; } = string.Empty;
        public sbyte IsDeleted { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
