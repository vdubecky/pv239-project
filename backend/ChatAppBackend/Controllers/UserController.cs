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
        
        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<bool> DeleteUser(int id)
        {
            return await userFacade.DeleteUser(id);
        }

        [HttpGet]
        //[Authorize]
        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            return userFacade.GetAllUsers();
        }
      
        //[Authorize]
        [HttpGet("{id}/contatcs")]
        public IEnumerable<UserDto> GetAllContacts(int id)
        {
            return userFacade.GetAllContacts(id);
        }
        
        //[Authorize]
        [HttpGet("{id}")]
        public async Task<UserDto?> GetUser(int id)
        {
            // var idFromClaims = User.GetUserIdFromClaims();
            
            return await userFacade.GetUser(id);
        }
    }
}
