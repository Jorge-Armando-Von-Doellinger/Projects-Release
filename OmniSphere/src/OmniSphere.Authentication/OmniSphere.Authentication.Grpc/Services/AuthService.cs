using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using OmniSphere.Authentication.Application.Interfaces.UseCases;
using OmniSphere.Authentication.Grpc.Exception;

namespace OmniSphere.Authentication.Grpc.Services;

public class AuthService : Grpc.AuthService.AuthServiceBase
{
    private readonly IAuthUseCase _useCase;

    public AuthService(IAuthUseCase useCase)
    {
        _useCase = useCase;
    }
    public override async Task<UserId> ValidateToken(JwtToken request, ServerCallContext context)
    {
        var userId = await _useCase.GetUserIdByTokenAsync(request.Token);
        return new() { UserId_ = userId };
    }

    public override async Task<UserId> ValidateHeaderToken(Empty request, ServerCallContext context)
    {
        var header = context.RequestHeaders.GetValue("Authorization") ?? throw new InvalidTokenException("Authorization header is missing");
        var token = header.StartsWith("Bearer ") ? header.Substring("Bearer ".Length) : header;
        var userId = await _useCase.GetUserIdByTokenAsync(token);
        Console.WriteLine(userId);
        return new() { UserId_ = userId };
    }

    public override async Task<JwtToken> GetJwtToken(UserCredentials request, ServerCallContext context)
    {
        var token = await _useCase.GenerateTokenAsync(request.Email, request.Password);
        return new() { Token = token };
    }
}