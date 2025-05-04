using pv239_project.Client;
using pv239_project.Models;
using UserDto = pv239_project.Client.UserDto;

namespace pv239_project.Mappers;

public static class EntityMapper
{
    public static UserUpdateDto ToUpdateUserDto(this UserDto userEntity)
    {
        return new UserUpdateDto
        {
            Firstname = userEntity.Firstname,
            Surname = userEntity.Surname,
            Email = userEntity.Email,
        };
    }

    public static User UserDtoToUser(this UserDto userDto)
    {
        return new User()
        {
            Id = userDto.Id,
            Firstname = userDto.Firstname,
            Surname = userDto.Surname,
            Email = userDto.Email,
            ProfilePicture = userDto.ProfilePicture,
        };
    }
}
