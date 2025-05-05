using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace ChatAppBackend.Auth;

public class AuthTokenHandler
{
    public string CreateNewToken(int userId)
    {
        var key = new SymmetricSecurityKey("M5\\,3c>\u00a3vAz<XIVhfihJYY![*&r4xKL4b\"4Y>4)F"u8.ToArray());
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "your_issuer", // TODO
            audience: "your_audience", // TODO
            claims: [new Claim(ClaimTypes.NameIdentifier, userId.ToString())],
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
