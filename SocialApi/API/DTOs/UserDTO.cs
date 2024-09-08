namespace API.DTOs
{
    public class UserDTO
    {
        public required string Alias { get; set; }
        public required string AuthToken { get; set; }
        public required int UserRoleId { get; set; }
        public required int UserTypeId { get; set; }

    }
}
