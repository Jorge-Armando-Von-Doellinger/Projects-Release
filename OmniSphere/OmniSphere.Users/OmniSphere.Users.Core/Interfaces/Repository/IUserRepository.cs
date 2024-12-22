using System.Linq.Expressions;
using OmniSphere.Users.Core.Entity;

namespace OmniSphere.Users.Core.Interfaces.Repository;

public interface IUserRepository
{
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(string id);
    Task<User?> FindUserByEmailAsync(string email);
    Task<int> CountUsersAsync();
}