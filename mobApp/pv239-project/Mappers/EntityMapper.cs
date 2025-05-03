using pv239_project.Models;

namespace pv239_project.Mappers;

public static class EntityMapper
{
    public static UpdateUserDto ToUpdateUserDto(this UserDto userEntity)
    {
        return new UpdateUserDto
        {
            Firstname = userEntity.Firstname,
            Surname = userEntity.Surname,
            Email = userEntity.Email,
        };
    }
}
