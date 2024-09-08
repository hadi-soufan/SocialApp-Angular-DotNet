namespace API.DTOs
{
    public class LoginDTO
    {
        public required string Alias { get; set; }
        public required string Creds { get; set; }
    }
}
