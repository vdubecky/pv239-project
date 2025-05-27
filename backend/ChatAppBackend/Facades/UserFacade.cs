using ChatAppBackend.Dtos;
using ChatAppBackend.Entities;
using ChatAppBackend.Mappers;
using ChatAppBackend.Services;

namespace ChatAppBackend.Facades
{
    public class UserFacade(UserService userService)
    {
        public async Task<bool> RegisterUser(UserRegisterDto userDto)
        {
            return await userService.RegisterUser(userDto.UserRegisterDtoToUserEntity());
        }

        public async Task<int?> LoginUser(UserLoginDto loginDto)
        {
            return await userService.LoginUser(loginDto);
        }

        /// <summary>
        /// Updates users profile.
        /// </summary>
        /// <param name="id">Id of the user to update.</param>
        /// <param name="userDto">The user dto with updated information.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        public async Task<bool> UpdateUserProfile(int id, UserUpdateDto userDto)
        {
            return await userService.UpdateUserProfile(id, userDto);
        }

        /// <summary>
        /// Changes a user password.
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="changeUserPasswordDto"></param>
        /// <returns></returns>
        public async Task<bool> ChangeUserPassword(int id, ChangeUserPasswordDto changeUserPasswordDto)
        {
            if(changeUserPasswordDto.NewPassword != changeUserPasswordDto.NewPasswordConfirm)
            {
                return false;
            }

            return await userService.ChangeUserPassword(id, changeUserPasswordDto.OldPassword, changeUserPasswordDto.NewPassword);
        }

        public async Task<bool> UploadUserPicture(int id, Stream imageData, string fileName)
        {
            return await userService.UploadUserPicture(id, imageData, fileName);
        }
        
        public async Task<bool> DeleteUser(int id)
        {
            return await userService.DeleteUser(id);
        }

        /// <summary>
        /// Returns all users.
        /// </summary>
        /// <returns>All users.</returns>
        public IEnumerable<UserDto> GetAllUsers()
        {
            IEnumerable<UserEntity> users = userService.GetAllUsers();
            return users.UserEntitiesToUserDtos();
        }
        
        public IEnumerable<UserDto> GetAllContacts(int id)
        {
            IEnumerable<UserEntity> users = userService.GetAllContacts(id);
            return users.UserEntitiesToUserDtos();
        }
        
        public async Task<UserDto?> GetUser(int id)
        {
            UserEntity? user = await userService.GetUser(id);
            return user?.UserEntityToUserDto();
        }
    }
}
