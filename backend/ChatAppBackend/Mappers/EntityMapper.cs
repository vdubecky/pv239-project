﻿using ChatAppBackend.Dtos;
using ChatAppBackend.Entities;
using ChatAppBackend.Extensions;

namespace ChatAppBackend.Mappers;

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
    
    public static AuthUserDto UserEntityToUserAuthDto(this UserEntity userEntity)
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

    public static IEnumerable<ConversationPreviewDto> ConEntityToConPreviewDtos(this IQueryable<ConversationEntity> conversationEntities, int currentUserId)
    {
        return conversationEntities
            .Select(c => new ConversationPreviewDto
            {
                ConversationId = c.Id,
                Name = GetConversationName(c.Members, currentUserId),
                LastMessage = c.LastMessage != null ? c.LastMessage.Content : string.Empty,
                ProfilePicture = GetProfilePictureBase64(c.Members.FirstOrDefault(m => m.UserId != currentUserId).UserEntity.ProfilePicture),
                LastMessageDate = c.LastMessage.SentAt
            });
    }

    public static string GetConversationName(IEnumerable<ConversationMember> members, int currentUserId)
    {
        ConversationMember? member = members.FirstOrDefault(m => m.UserId != currentUserId);

        return member == null ? "Unknown conversation" : $"{member.UserEntity.Firstname} {member.UserEntity.Surname}";
    }

    public static ConversationDto ConEntityToConDto(this ConversationEntity conversationEntity)
    {
        return new ConversationDto
        {
            Id = conversationEntity.Id,
            Name = conversationEntity.Name,
            Messages = conversationEntity.Messages.MessageEntitiesToMessageDtos(),
            Members = conversationEntity.Members.MemberEntitiesToMemberDtos(),
            
        };
    }

    public static IEnumerable<MessageDto> MessageEntitiesToMessageDtos(this IEnumerable<MessageEntity> messageEntities)
    {
        List<MessageDto> messageDtos = new();
        foreach (MessageEntity message in messageEntities)
        {
            messageDtos.Add(new MessageDto
            {
                Id = message.Id,
                SenderId = message.SenderId,
                Content = message.Content,
                LastMessageDate = message.SentAt
            });
        }
        return messageDtos;
    }

    public static IEnumerable<MemberDto> MemberEntitiesToMemberDtos(this IEnumerable<ConversationMember> memberEntities)
    {
        List<MemberDto> memberDtos = new();
        foreach (ConversationMember member in memberEntities)
        {
            memberDtos.Add(new MemberDto
            {
                Id = member.UserId,
                FirstName = member.UserEntity.Firstname,
                Surname = member.UserEntity.Surname,
            });
        }
        return memberDtos;
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