using ChatAppBackend.Dtos;
using ChatAppBackend.Entities;

namespace ChatAppBackend.Mappers
{
    public static class DtoMapper
    {
        public static UserEntity UserDtoToUserEntity(this UserDto userDto)
        {
            return new()
            {
                Id = userDto.Id,
                Firstname = userDto.Firstname,
                Surname = userDto.Surname,
                Email = userDto.Email,
                ProfilePicture = userDto.ProfilePicture
            };
        }

        public static UserEntity UserUpdateDtoToUserEntity(this UserUpdateDto userUpdateDto)
        {
            return new()
            {
                Firstname = userUpdateDto.Firstname,
                Surname = userUpdateDto.Surname,
                Email = userUpdateDto.Email,
                ProfilePicture = userUpdateDto.ProfilePicture
            };
        }

        public static UserEntity UserRegisterDtoToUserEntity(this UserRegisterDto registerDto)
        {
            return new()
            {
                Firstname = registerDto.Firstname,
                Surname = registerDto.Surname,
                Email = registerDto.Email,
                PasswordHash = registerDto.Password,
                ProfilePicture = registerDto.ProfilePicture
            };
        }

        public static IEnumerable<UserEntity> UserDtosToUserEntities(this IEnumerable<UserDto> userDtos)
        {
            List<UserEntity> userEntities = new();

            foreach (UserDto user in userDtos)
            {
                userEntities.Add(user.UserDtoToUserEntity());
            }

            return userEntities;
        }
    }
}
