using OmniSphere.Users.Application.DTOs;
using OmniSphere.Users.Application.Interfaces.UseCases;
using OmniSphere.Users.Application.Mapper;
using OmniSphere.Users.Core.Interfaces.Hasher;
using OmniSphere.Users.Core.Interfaces.Repository;

namespace OmniSphere.Users.Application.Implementations.UseCases;

public class UserUseCase : IUserUseCase
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly UserMapper _mapper;

    public UserUseCase(IUserRepository repository,
        IPasswordHasher passwordHasher,
        UserMapper mapper)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }
    public async Task AddUserAsync(UserDTO dto)
    {
        var entity = _mapper.MapToUser(dto);
        entity.Password = _passwordHasher.HashPassword(entity.Password);
        await _repository.AddUserAsync(entity);
    }

    public async Task UpdateUserAsync(UserWithIdDTO dto)
    {
        var entity = _mapper.MapToUser(dto);
        await _repository.UpdateUserAsync(entity);
    }

    public async Task DeleteUserAsync(string id)
    {
        await _repository.DeleteUserAsync(id);
    }

    public async Task<string?> GetUserIdAsync(string email, string password)
    {
        var passwordHash = _passwordHasher.HashPassword(password);
        var user = await _repository.FindUserByEmailAsync(email);
        var valid = _passwordHasher.VerifyHashedPassword(user.Password, password);
        return valid ? user.Id : null;
    }
    

    public async Task<int> GetUserCountAsync() => await _repository.CountUsersAsync();
    
}