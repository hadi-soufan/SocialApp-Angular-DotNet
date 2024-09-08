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

        public int UserTypeId { get; set; }
        public required UserType UserType { get; set; }

        // Each user has one role
        public int RoleId { get; set; }
        public required Role Role { get; set; }
    }
}
