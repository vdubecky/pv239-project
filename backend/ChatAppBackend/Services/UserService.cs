using ChatAppBackend.Dtos;
using ChatAppBackend.Entities;
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
            user.Password = passwordHasher.HashPassword(user, user.Password);

            dbContext.Users.Add(user);
            return await dbContext.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns> True if the user was logged successfully, false otherwise.</returns>
        public async Task<bool> LoginUser(UserLoginDto loginDto)
        {
            UserEntity user = dbContext.Users.FirstOrDefault(u => u.Email == loginDto.Email);

            if (user == null)
            {
                return false;
            }

            PasswordVerificationResult verificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, loginDto.Password);
            return verificationResult == PasswordVerificationResult.Success;
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
            toUpdate.ProfilePicture = user.ProfilePicture;

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

            PasswordVerificationResult verificationResult = passwordHasher.VerifyHashedPassword(toUpdate, toUpdate.Password, oldPassword);
            if(verificationResult != PasswordVerificationResult.Success)
            {
                return false;
            }

            toUpdate.Password = passwordHasher.HashPassword(toUpdate, newPassword);
            dbContext.Users.Update(toUpdate);

            return await dbContext.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// </summary>
        /// <returns> All users in the database.</returns>
        public IEnumerable<UserEntity> GetAllUsers()
        {
            return dbContext.Users;
        }
        
        public async Task<UserEntity?> GetUser(int id)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
