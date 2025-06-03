using pv239_project.Database.Interfaces;
using pv239_project.Entities;
using SQLite;

namespace pv239_project.Database;

public class UserDatabase : IUserDatabase
{
    private SQLiteAsyncConnection? _database;

    private async Task Init()
    {
        if (_database != null)
        {
            return;
        }

        _database = new SQLiteAsyncConnection(MauiProgram.DatabasePath, MauiProgram.Flags);
        await _database.CreateTableAsync<UserEntity>();
    }

    public async Task<UserEntity?> GetActualUser()
    {
        await Init();
        return await _database!.Table<UserEntity>().FirstOrDefaultAsync(); // Don't suppress nullability warnings, add explicit null checks instead
    }

    public async Task SaveUser(UserEntity user)
    {
        await Init();
        await _database!.DeleteAllAsync<UserEntity>();
        await _database.InsertAsync(user);
    }

    public async Task DeleteUser()
    {
        await _database!.DeleteAllAsync<UserEntity>();
    }
}
