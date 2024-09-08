using System;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string PasswordHash { get; set; }
        public required string PasswordSalt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
        public sbyte IsDeleted { get; set; } = 0; 
    }
}
