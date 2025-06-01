using ChatAppBackend.Dtos;
using ChatAppBackend.Facades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppBackend.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController(UserFacade userFacade) : ControllerBase
    {
        [HttpPost]
        public async Task<bool> CreateAccount(UserRegisterDto userDto)
        {
            return await userFacade.RegisterUser(userDto);
        }
        
        [Authorize]
        [HttpPut("{id}")]
        public async Task<bool> UpdateUserProfile(int id, UserUpdateDto userDto)
        {
            return await userFacade.UpdateUserProfile(id, userDto);
        }
        
        [Authorize]
        [HttpPut("{id}/changePassword")]
        public async Task<bool> ChangeUserPassword(int id, ChangeUserPasswordDto changeUserPasswordDto)
        {
            return await userFacade.ChangeUserPassword(id, changeUserPasswordDto);
        }
        
        [Authorize]
        [HttpPut("{id}/picture")]
        public async Task<bool> UploadUserPicture(int id, [FromForm] UploadUserPictureDto uploadDto)
        {
            return await userFacade.UploadUserPicture(id, uploadDto.File.OpenReadStream(), uploadDto.FileName);
        }
        
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<bool> DeleteUser(int id)
        {
            return await userFacade.DeleteUser(id);
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            return userFacade.GetAllUsers();
        }
      
        [Authorize]
        [HttpGet("{id}/contatcs")]
        public IEnumerable<UserDto> GetAllContacts(int id)
        {
            return userFacade.GetAllContacts(id);
        }
        
        [Authorize]
        [HttpGet("{id}")]
        public async Task<UserDto?> GetUser(int id)
        {
            // var idFromClaims = User.GetUserIdFromClaims();
            
            return await userFacade.GetUser(id);
        }
    }
}
