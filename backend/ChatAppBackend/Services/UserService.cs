using ChatAppBackend.Dtos;
using ChatAppBackend.Entities;

namespace ChatAppBackend.Services
{
    public class UserService(ChatAppDbContext dbContext)
    {
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns> True if the user was registered successfully, false otherwise.</returns>
        public async Task<bool> RegisterUser(UserEntity user)
        {
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

            return user.Password == loginDto.Password;
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
        /// </summary>
        /// <returns> All users in the database.</returns>
        public IEnumerable<UserEntity> GetAllUsers()
        {
            return dbContext.Users;
        }
    }
}
