using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using intern_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace intern_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RDS_AuthController : ControllerBase
{

    private readonly IConfiguration _configuration;
    private readonly AppDbContext _dynamoDbContext;

    public RDS_AuthController(IConfiguration configuration, AppDbContext dynamoDbContext)
    {
        _configuration = configuration;
        _dynamoDbContext = dynamoDbContext;
    }


    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserDto request)
    {
        CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        User? user = new User();

        user.Username = request.Username;
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        await _dynamoDbContext.users.AddAsync(user);
        await _dynamoDbContext.SaveChangesAsync();
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UserDto request)
    {
        var user = _dynamoDbContext.users.FirstOrDefault(x => x.Username == request.Username);
        if (user?.Username != request.Username)
        {
            return Unauthorized("User not found." + user.Username);
        }

        if (request.Password != null &&
            !VerifyPasswordHash(user, request.Password, user?.PasswordHash, user.PasswordSalt))
        {
            return Unauthorized("Wrong Password.");
        }

        string token = CreateToken(user);
        return Ok(token);
    }

    private string CreateToken(User? user)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Username)
        };

        var key = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: cred
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
    private bool VerifyPasswordHash(User? user, string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(user.PasswordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}