using System.Text.Json;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using OmniSphere.Users.Application.Interfaces.UseCases;
using OmniSphere.Users.Grpc.Mapper;

namespace OmniSphere.Users.Grpc.Services;

public class UserServices : UsersProtoService.UsersProtoServiceBase
{
    private readonly IUserUseCase _useCase;
    private readonly UserProtoMapper _mapper;

    //IUserUseCase useCase, UserProtoMapper mapper
    public UserServices()
    {
        //_useCase = useCase;
        // _mapper = mapper;
    }
    public override async Task<Empty> AddUser(UserProtoDto request, ServerCallContext context)
    {
        // var dto = _mapper.ToUserDTO(request);
        // await _useCase.AddUserAsync(dto);
        Console.WriteLine("Batata");
        return new Empty();
    }

    public override async Task<Empty> UpdateUser(UserProtoWithIdDto request, ServerCallContext context)
    {
        var dto = _mapper.ToUserDTO(request);
        await _useCase.UpdateUserAsync(dto);
        return new Empty();
    }

    public override async Task<Empty> DeleteUser(UserId request, ServerCallContext context)
    {
        await _useCase.DeleteUserAsync(request.Id);
        return new Empty();
    }

    public override async Task<CountUser> GetCountUsers(Empty request, ServerCallContext context)
    {
        var count = await _useCase.GetUserCountAsync();
        return new() { Counter = count };
    }
    
    public override async Task<UserId> FindUser(UserFinder request, ServerCallContext context)
    {
        await Task.Run(() => Console.WriteLine(JsonSerializer.Serialize(request)));
        var id = await _useCase.GetUserIdAsync(request.Email, request.Password);
        var userid = new UserId() { Id = id };
        return userid;
    }
}   