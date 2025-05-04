using ChatAppBackend.Dtos;
using ChatAppBackend.Entities;

namespace ChatAppBackend.Mappers
{
    public static class EntityMapper
    {
        public static UserDto UserEntityToUserDto(this UserEntity userEntity)
        {
            return new()
            {
                Id = userEntity.Id,
                Firstname = userEntity.Firstname,
                Surname = userEntity.Surname,
                Email = userEntity.Email,
                ProfilePicture = "data:image/jpeg;charset=utf-8;base64," + userEntity.ProfilePicture
            };
        }

        public static IEnumerable<UserDto> UserEntitiesToUserDtos(this IEnumerable<UserEntity> userEntities)
        {
            List<UserDto> userDtos = new();

            foreach (UserEntity user in userEntities)
            {
                userDtos.Add(user.UserEntityToUserDto());
            }

            return userDtos;
        }
    }
}
