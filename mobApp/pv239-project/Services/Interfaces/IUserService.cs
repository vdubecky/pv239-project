using pv239_project.Models;

namespace pv239_project.Services.Interfaces;

public interface IUserService
{
    Task<ICollection<UserDto>> GetAllUsers();
    Task<UserDto?> GetUser(int id);
    Task UpdateUser(int id, UpdateUserDto updateUserDto);
    Task ChangePassword(int id, ChangePasswordDto dto);
}
