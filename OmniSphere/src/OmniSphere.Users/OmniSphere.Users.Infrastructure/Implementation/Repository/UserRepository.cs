using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OmniSphere.Users.Core.Entity;
using OmniSphere.Users.Core.Interfaces.Repository;
using OmniSphere.Users.Infrastructure.Context;

namespace OmniSphere.Users.Infrastructure.Implementation.Repository;

public class UserRepository : IUserRepository
{
    private readonly UserContext _context;

    public UserRepository(UserContext context)
    {
        _context = context;
    }
    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(string id)
    {
        _context.Users.Remove(new() { Id = id });
        await _context.SaveChangesAsync();
    }

    public async Task<User?> FindUserByEmailAsync(string email)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<int> CountUsersAsync()
    {
        return await _context.Users
            .AsNoTracking()
            .CountAsync();
    }
}