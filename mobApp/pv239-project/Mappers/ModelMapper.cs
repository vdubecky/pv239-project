using CommunityToolkit.Maui.Core.Extensions;
using pv239_project.Client;
using pv239_project.Entities;
using pv239_project.Models;
using UserDto = pv239_project.Client.UserDto;

namespace pv239_project.Mappers;

public static class ModelMapper
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
        return new User
        {
            Id = userDto.Id,
            Firstname = userDto.Firstname,
            Surname = userDto.Surname,
            Email = userDto.Email,
            ProfilePicture = userDto.ProfilePicture,
        };
    }

    public static User EntityToUser(this UserEntity userEntity)
    {
        return new User
        {
            Id = userEntity.UserId,
            Firstname = userEntity.Firstname,
            Surname = userEntity.Surname,
            Email = userEntity.Email,
            ProfilePicture = userEntity.ProfilePicture
        };
    }

    public static ConversationDetail ConversationDtoToDetail(this ConversationDto conversationDto, int actualId)
    {
        return new ConversationDetail()
        {
            Id = conversationDto.Id,
            Members = conversationDto.Members.MemberDtosToMembers().ToObservableCollection(),
            Messages = conversationDto.Messages.MessageDtosToMessages(actualId).ToObservableCollection(),
        };
    }

    public static ConversationPreview PreviewDtoToPreview(this ConversationPreviewDto conversationPreviewDto)
    {
        return new ConversationPreview
        {
            ConversationId = conversationPreviewDto.ConversationId,
            Title = conversationPreviewDto.Name,
            LastMessage = conversationPreviewDto.LastMessage,
            ProfilePicture = conversationPreviewDto.ProfilePicture,
            LastMessageTime = conversationPreviewDto.LastMessageDate
        };
    }

    private static Member MemberDtoToMember(this MemberDto memberDto)
    {
        return new Member
        {
            Id = memberDto.Id,
            FirstName = memberDto.FirstName,
            Surname = memberDto.Surname,
        };
    }

    private static Message MessageDtoToMessage(this MessageDto messageDto, int actualId)
    {
        return new Message()
        {
            Id = messageDto.Id,
            SenderId = messageDto.SenderId,
            Content = messageDto.Content,
            IsOutgoing = messageDto.SenderId == actualId
        };
    }

    private static IEnumerable<Member> MemberDtosToMembers(this ICollection<MemberDto> members)
    {
        return members.Select(m => m.MemberDtoToMember());
    }

    private static IEnumerable<Message> MessageDtosToMessages(this ICollection<MessageDto> messages, int actualId)
    {
        return messages.Select(m => m.MessageDtoToMessage(actualId));
    }
}
