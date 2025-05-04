using ChatAppBackend.Auth;
using ChatAppBackend.Dtos;
using ChatAppBackend.Facades;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppBackend.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthenticationController(UserFacade userFacade, AuthTokenHandler tokenHandler)
{
    [HttpPost("login")]
    public async Task<string?> Login(UserLoginDto loginDto)
    {
        var userId = await userFacade.LoginUser(loginDto);

        if (userId is null)
        {
            return null;
        }

        return tokenHandler.CreateNewToken(userId.Value);
    }
}
