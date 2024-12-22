using OmniSphere.Users.Application.DTOs;

namespace OmniSphere.Users.Application.Interfaces.UseCases;

public interface IUserUseCase
{
    Task AddUserAsync(UserDTO dto);
    Task UpdateUserAsync(UserWithIdDTO dto);
    Task DeleteUserAsync(string id);
    Task<string?> GetUserIdAsync(string email, string password);
    Task<int> GetUserCountAsync();
}