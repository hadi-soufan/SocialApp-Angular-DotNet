using System;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController : BaseApiController
{
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;

    public AccountController(DataContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
    {

        if (await UserExists(registerDTO.Alias)) return BadRequest("Username exists");

        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            UserName = registerDTO.Alias.ToLower(),
            PasswordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Creds))),
            PasswordSalt = Convert.ToBase64String(hmac.Key)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new UserDTO
        {
            Alias = user.UserName,
            AuthToken = _tokenService.CreateToken(user)
        };
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.UserName == loginDTO.Alias.ToLower());

        if (user is null) return Unauthorized("Invalid Credentials");

        using var hmac = new HMACSHA512(Convert.FromBase64String(user.PasswordSalt));

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Creds));
        var computedHashBase64 = Convert.ToBase64String(computedHash);

        if (computedHashBase64 != user.PasswordHash)
            return Unauthorized("Invalid Credentials");

        return new UserDTO 
        {
            Alias = user.UserName,
            AuthToken = _tokenService.CreateToken(user) 
        };
    }

    private async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
    }
}
