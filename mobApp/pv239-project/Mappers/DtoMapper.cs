using pv239_project.Client;
using pv239_project.Models;


namespace pv239_project.Mappers;

public static class DtoMapper
{
    public static CreateMessageDto MessageToDto(this Message message)
    {
        return new CreateMessageDto
        {
            SenderId = message.SenderId,
            Content = message.Content
        };
    }

    public static UserUpdateDto UserToUserUpdateDto(this User user)
    {
        return new UserUpdateDto
        {
            Email = user.Email,
            Firstname = user.Firstname,
            Surname = user.Surname,
            ProfilePicture = user.ProfilePicture
        };
    }
}