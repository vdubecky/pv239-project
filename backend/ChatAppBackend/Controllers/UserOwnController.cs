using ChatAppBackend.Auth;
using ChatAppBackend.Dtos;
using ChatAppBackend.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppBackend.Controllers;

[ApiController]
[Route("api/v1/users/own")]
public class UserOwnController(UserFacade userFacade) : ControllerBase
{
    [Authorize]
    [HttpPut]
    public async Task<bool> UpdateUserProfile(UserUpdateDto userDto)
    {
        var id = User.GetUserIdFromClaims();
        return await userFacade.UpdateUserProfile(id!.Value, userDto);
    }

    [Authorize]
    [HttpPut("changePassword")]
    public async Task<bool> ChangeUserPassword(ChangeUserPasswordDto changeUserPasswordDto)
    {
        var id = User.GetUserIdFromClaims();
        return await userFacade.ChangeUserPassword(id!.Value, changeUserPasswordDto);
    }
    
    [Authorize]
    [HttpPut("{id}/picture")]
    public async Task<bool> UploadUserPicture([FromForm] UploadUserPictureDto uploadDto)
    {
        var id = User.GetUserIdFromClaims();
        return await userFacade.UploadUserPicture(id!.Value, uploadDto.File.OpenReadStream());
    }
}
