using System.Security;
using ChatAppBackend.Dtos;
using ChatAppBackend.Entities;
using ChatAppBackend.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChatAppBackend.Services
{
    public class UserService(ChatAppDbContext dbContext, IPasswordHasher<UserEntity> passwordHasher)
    {
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns> True if the user was registered successfully, false otherwise.</returns>
        public async Task<bool> RegisterUser(UserEntity user)
        {
            user.PasswordHash = passwordHasher.HashPassword(user, user.PasswordHash);

            dbContext.Users.Add(user);
            return await dbContext.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns> UserEntity if the user was logged successfully</returns>
        public async Task<UserEntity> LoginUser(UserLoginDto loginDto)
        {
            var userEntity = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (userEntity is null)
            {
                throw new Exception("User not found");
            }

            PasswordVerificationResult verificationResult =
                passwordHasher.VerifyHashedPassword(userEntity, userEntity.PasswordHash, loginDto.Password);

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                throw new SecurityException("Credentials do not match");
            }

            return userEntity;
        }

        /// <summary>
        /// Updates a user's profile information.
        /// </summary>
        /// <param name="id"> User id of entity to be updated.</param>
        /// <param name="user">The user entity with updated information.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        public async Task<bool> UpdateUserProfile(int id, UserUpdateDto user)
        {
            UserEntity toUpdate = dbContext.Users.FirstOrDefault(u => u.Id == id);

            if (toUpdate == null)
            {
                return false;
            }

            toUpdate.Firstname = user.Firstname;
            toUpdate.Surname = user.Surname;
            toUpdate.Email = user.Email;

            dbContext.Users.Update(toUpdate);
            return await dbContext.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Changes a user password.
        /// </summary>
        /// <param name="id"> User id</param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns> True if the password was changed successfully, false otherwise.</returns>
        public async Task<bool> ChangeUserPassword(int id, string oldPassword, string newPassword)
        {
            UserEntity? toUpdate = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (toUpdate is null)
            {
                return false;
            }

            PasswordVerificationResult verificationResult =
                passwordHasher.VerifyHashedPassword(toUpdate, toUpdate.PasswordHash, oldPassword);
            if (verificationResult != PasswordVerificationResult.Success)
            {
                return false;
            }

            toUpdate.PasswordHash = passwordHasher.HashPassword(toUpdate, newPassword);
            dbContext.Users.Update(toUpdate);

            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UploadUserPicture(int id, Stream imageData, string fileName)
        {
            UserEntity? toUpdate = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (toUpdate is null)
            {
                return false;
            }

            if (toUpdate.ProfilePicture is not null)
            {
                File.Delete(toUpdate.ProfilePicture);
            }

            using var memoryStream = new MemoryStream();
            await imageData.CopyToAsync(memoryStream);
            byte[] bytes = memoryStream.ToArray();
            await bytes.SaveByteArrayToFile($"profile-pictures/{fileName}");

            toUpdate.ProfilePicture = $"profile-pictures/{fileName}";

            dbContext.Users.Update(toUpdate);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUser(int id)
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync();
            List<ConversationEntity> conversations = await dbContext.Conversations
                .Where(c => c.Members.Any(m => m.UserId == id))
                .ToListAsync();
            IEnumerable<int> conversationIds = conversations.Select(c => c.Id);
            var conversationMembers = await dbContext.ConversationMembers
                .Where(c => conversationIds.Contains(c.ConversationId))
                .ToListAsync();
            List<MessageEntity> messages = await dbContext.Messages
                .Where(m => conversationIds.Contains(m.ConversationId))
                .ToListAsync();

            UserEntity? toUpdate = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (toUpdate is null)
            {
                return false;
            }

            if (toUpdate.ProfilePicture is not null)
            {
                File.Delete(toUpdate.ProfilePicture);
            }

            dbContext.Messages.RemoveRange(messages);
            await dbContext.SaveChangesAsync();
            
            dbContext.ConversationMembers.RemoveRange(conversationMembers);
            dbContext.Conversations.RemoveRange(conversations);
            await dbContext.SaveChangesAsync();
            
            dbContext.Users.Remove(toUpdate);
            var deleted = await dbContext.SaveChangesAsync() > 0;

            await transaction.CommitAsync();
            return deleted;
        }

        /// <summary>
        /// </summary>
        /// <returns> All users in the database.</returns>
        public IEnumerable<UserEntity> GetAllUsers()
        {
            return dbContext.Users;
        }

        public IEnumerable<UserEntity> GetAllContacts(int id)
        {
            // TODO: Return all contacts + optional conversation id with user
            return dbContext.Users.Where(u => u.Id != id);
        }

        public async Task<UserEntity?> GetUser(int id)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
