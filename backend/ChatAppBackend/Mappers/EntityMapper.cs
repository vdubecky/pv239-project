using ChatAppBackend.Dtos;
using ChatAppBackend.Entities;
using ChatAppBackend.Extensions;

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
                ProfilePicture = GetProfilePictureBase64(userEntity.ProfilePicture),
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

        private static string? GetProfilePictureBase64(string? profilePicturePath)
        {
            if (profilePicturePath is null)
            {
                return null;
            }

            var contentType = Path.GetExtension(profilePicturePath).MapExtensionToContentType();
            byte[] imageData = profilePicturePath.ReadByteArrayFromFile();
            var base64String = Convert.ToBase64String(imageData);
            return $"data:{contentType};charset=utf-8;base64,{base64String}";
        }
    }
}
