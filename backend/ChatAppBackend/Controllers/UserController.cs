using ChatAppBackend.Dtos;
using ChatAppBackend.Facades;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppBackend.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController(UserFacade userFacade)
    {
        [HttpPost]
        public async Task<bool> CreateAccount(UserRegisterDto userDto)
        {
            return await userFacade.RegisterUser(userDto);
        }

        [HttpPost("login")]
        public async Task<bool> Login(UserLoginDto loginDto)
        {
            return await userFacade.LoginUser(loginDto);
        }

        [HttpPut("{id}")]
        public async Task<bool> UpdateUserProfile(int id, UserUpdateDto userDto)
        {
            return await userFacade.UpdateUserProfile(id, userDto);
        }

        [HttpPut("{id}/changePassword")]
        public async Task<bool> ChangeUserPassword(int id, ChangeUserPasswordDto changeUserPasswordDto)
        {
            return await userFacade.ChangeUserPassword(id, changeUserPasswordDto);
        }

        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            return userFacade.GetAllUsers();
        }
        
        [HttpGet("{id}")]
        public async Task<UserDto?> GetUser(int id)
        {
            return await userFacade.GetUser(id);
        }
    }
}
