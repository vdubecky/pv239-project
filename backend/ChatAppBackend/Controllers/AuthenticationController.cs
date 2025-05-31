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
    public async Task<AuthUserDto> Login(UserLoginDto loginDto)
    {
        var user = await userFacade.LoginUser(loginDto);
        user.Token = tokenHandler.CreateNewToken(user.Id);

        return user;
    }
}
