using CommunityToolkit.Maui.Core.Extensions;
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

    public static ConversationDetail ConversationDtoToDetail(this ConversationDto conversationDto)
    {
        return new ConversationDetail()
        {
            Id = conversationDto.Id,
            Name = conversationDto.Name,
            Members = conversationDto.Members.MemberDtosToMembers().ToObservableCollection(),
            Messages = conversationDto.Messages.MessageDtosToMessages().ToObservableCollection(),
        };
    }

    public static Member MemberDtoToMember(this MemberDto memberDto)
    {
        return new Member()
        {
            Id = memberDto.Id,
            FirstName = memberDto.FirstName,
            Surname = memberDto.Surname,
        };
    }

    public static Message MessageDtoToMessage(this MessageDto messageDto)
    {
        return new Message()
        {
            Id = messageDto.Id,
            SenderId = messageDto.SenderId,
            Content = messageDto.Content,
        };
    }

    public static IEnumerable<Member> MemberDtosToMembers(this ICollection<MemberDto> members)
    {
        return members.Select(m => new Member()
        {
            Id = m.Id,
            FirstName = m.FirstName,
            Surname = m.Surname,
        });
    }

    public static IEnumerable<Message> MessageDtosToMessages(this ICollection<MessageDto> messages)
    {
        return messages.Select(m => new Message()
        {
            Id = m.Id,
            SenderId = m.SenderId,
            Content = m.Content,
        });
    }
}
