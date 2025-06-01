using pv239_project.Entities;
using pv239_project.Models;

namespace pv239_project.Mappers;

public static class EntityMapper
{
    public static UserEntity ToEntity(this User user)
    {
        return new UserEntity
        {
            UserId = user.Id,
            Email = user.Email,
            Firstname = user.Firstname,
            Surname = user.Surname,
            ProfilePicture = user.ProfilePicture
        };
    }
}