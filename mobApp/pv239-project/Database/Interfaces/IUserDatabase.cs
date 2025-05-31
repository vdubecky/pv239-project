using pv239_project.Entities;

namespace pv239_project.Database.Interfaces;

public interface IUserDatabase
{
    Task<UserEntity?> GetActualUser();
    Task SaveUser(UserEntity user);
    Task DeleteUser();
}