using pv239_project.Models;

namespace pv239_project.Services.Interfaces;

public interface IUserService
{
    User? CurrentUser { get; }
    int CurrentUserId { get; }
    
    /// <summary>
    /// Logs in the user with the provided email and password.
    /// If successful, stores the JWT token in secure storage and updates the CurrentUser property.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns>JWT token</returns>
    Task<string> Login(string email, string password);
    
    /// <summary>
    /// Registers a new user with the provided details.
    /// If successful, logs in the user and stores the JWT token in secure storage.
    /// </summary>
    /// <param name="firstname"></param>
    /// <param name="surname"></param>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns>JWT token</returns>
    Task<string> Register(string firstname, string surname, string email, string password);
    
    Task LogOut();
    Task UpdateUserSettings(User updatedUser);
    Task DeleteUser();
    Task Init();
}