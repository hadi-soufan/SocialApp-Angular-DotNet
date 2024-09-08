namespace API.Entities
{
    public class UserType
    {
        public int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public sbyte IsDeleted { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<AppUser> Users { get; set; } = new List<AppUser>();
    }
}
