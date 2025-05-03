using System.Text;
using System.Text.Json;
using pv239_project.Models;
using pv239_project.Services.Interfaces;

namespace pv239_project.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://192.168.0.154:5115/api/v1/users/");
        // _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", yourAccessToken); TODO: Add later
    }

    public async Task<ICollection<UserDto>> GetAllUsers()
    {
        var response = await _httpClient.GetAsync(""); // Adjust route
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<UserDto>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
            return users ?? [];
        }

        return [];
    }

    public async Task<UserDto?> GetUser(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<UserDto>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );
                return user;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return null;
    }

    public async Task UpdateUser(int id, UpdateUserDto user)
    {
        try
        {
            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"{id}", content);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task ChangePassword(int id, ChangePasswordDto dto)
    {
        try
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"{id}/changePassword", content);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
