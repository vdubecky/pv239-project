using ChatAppBackend.Dtos;
using ChatAppBackend.Entities;

namespace ChatAppBackend.Mappers;

public static class DtoMapper
{
    public static UserEntity UserRegisterDtoToUserEntity(this UserRegisterDto registerDto)
    {
        return new UserEntity
        {
            Firstname = registerDto.Firstname,
            Surname = registerDto.Surname,
            Email = registerDto.Email,
            PasswordHash = registerDto.Password,
            ProfilePicture = registerDto.ProfilePicture
        };
    }
}