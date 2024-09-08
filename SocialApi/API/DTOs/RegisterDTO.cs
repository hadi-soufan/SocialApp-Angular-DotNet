using System;

namespace API.DTOs;

public class RegisterDTO
{
    public required string Alias { get; set; }
    public required string Creds { get; set; }
}
