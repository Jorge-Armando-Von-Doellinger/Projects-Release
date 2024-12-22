using OmniSphere.Users.Application.DTOs;
using OmniSphere.Users.Core.Entity;

namespace OmniSphere.Users.Application.Mapper;

public class UserMapper
{
    internal User MapToUser(UserWithIdDTO dto)
    {
        return new()
        {
            Email = dto.Email,
            Password = dto.Password,
            Username = dto.Username,
            Id = dto.Id
        };
    }
    internal User MapToUser(UserDTO dto)
    {
        return new()
        {
            Email = dto.Email,
            Password = dto.Password,
            Username = dto.Username
        };
    }

    internal UserWithIdDTO MapToUserWithIdDTO(User user)
    {
        return new()
        {
            Email = user.Email,
            Password = user.Password,
            Username = user.Username,
            Id = user.Id
        };
    }
}