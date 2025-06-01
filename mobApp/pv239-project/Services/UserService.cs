using pv239_project.Client;
using pv239_project.Database.Interfaces;
using pv239_project.Mappers;
using pv239_project.Models;
using pv239_project.Services.Interfaces;

namespace pv239_project.Services;

public class UserService(IUserClient userClient, IAuthenticationClient authenticationClient, IUserDatabase userDatabase) : IUserService
{
    public const string JwtTokenKey = "jwt_token";
    
    public User? CurrentUser { get; private set; }

    public int CurrentUserId => CurrentUser?.Id ?? 0;


    public async Task Init()
    {
        var entity = await userDatabase.GetActualUser();
        if (entity == null)
        {
            return;
        }
        
        CurrentUser = entity.EntityToUser();
    }
    
    public async Task<string> Login(string email, string password)
    {
        var loginDto = new UserLoginDto
        {
            Email = email,
            Password = password
        };

        var user = await authenticationClient.Authentication_LoginAsync(loginDto);
        var userModel = user.UserDtoToUser();
        
        await SecureStorage.SetAsync(JwtTokenKey, user.Token);
        await userDatabase.SaveUser(userModel.ToEntity());

        CurrentUser = userModel;
        return user.Token;
    }
    
    public async Task<string> Register(string firstname, string surname, string email, string password)
    {   
        var registerDto = new UserRegisterDto
        {
            Firstname = firstname,
            Surname = surname,
            Email = email,
            Password = password,
        };
        
        await userClient.User_CreateAccountAsync(registerDto);
        var token = await Login(email, password);

        return token;
    }

    public async Task LogOut()
    {
        SecureStorage.Remove(JwtTokenKey);
        await userDatabase.DeleteUser();
        
        CurrentUser = null;
    }

    public async Task UpdateUserSettings(User updatedUser)
    {
        await userClient.User_UpdateUserProfileAsync(CurrentUser.Id, updatedUser.UserToUserUpdateDto());
        await userDatabase.SaveUser(updatedUser.ToEntity());
        
        CurrentUser = updatedUser;
    }

    public async Task DeleteUser()
    {
        await userClient.User_DeleteUserAsync(CurrentUser.Id);
        await LogOut();
    }
}