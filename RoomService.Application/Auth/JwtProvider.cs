using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RoomService.Application.Interfaces.Repositories;
using RoomService.Domain.Entities;

namespace RoomService.Application.Auth;

public class JwtProvider : IJwtProvider
{
    private readonly IConfiguration  _configuration;

    public JwtProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GenerateToken(UserEntity userEntity)
    {
        var secretKey = _configuration.GetSection("AuthSettings").GetValue<string>("SecretKey");
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, userEntity.Email),
            new Claim(ClaimTypes.NameIdentifier, userEntity.Id.ToString())
        };

        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)), SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            expires: DateTime.UtcNow.AddDays(7),
            claims: claims,
            signingCredentials: signingCredentials);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}