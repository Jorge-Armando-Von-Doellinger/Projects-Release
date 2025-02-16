using OmniSphere.Users.Application.DTOs;
using OmniSphere.Users.Core.Entity;

namespace OmniSphere.Users.Grpc.Mapper;

public class UserProtoMapper
{
    internal UserDTO ToUserDTO(UserProtoDto user)
    {
        return new()
        {
            Email = user.Email,
            Password = user.Password,
            Username = user.Username
        };
    }

    internal UserWithIdDTO ToUserDTO(UserProtoWithIdDto user)
    {
        return new()
        {
            Email = user.Email,
            Password = user.Password,
            Username = user.Username,
            Id = user.Id
        };
    }

    internal UserProtoWithIdDto ToUserProtoWithIdDto(UserWithIdDTO dto)
    {
        return new()
        {
            Email = dto.Email,
            Password = dto.Password,
            Username = dto.Username,
            Id = dto.Id
        };
    }
}