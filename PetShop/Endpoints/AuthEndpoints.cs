using PetShop.Domain.Repositories;
using PetShop.Dto;
using PetShop.Utils;

namespace PetShop.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("login", Login);
    }

    private static async Task<IResult> Login(LoginDto loginDto, IUserRepository userRepository)
    {
        var user = await userRepository.GetByEmail(loginDto.Email);
        if (user == null) return Results.BadRequest(new { Error = "Email or password incorrects" });
        
        if(!AuthUtil.VerifyPass(user.Password, loginDto.Password)) return Results.BadRequest(new { Error = "Email or password incorrects" });

        var token = AuthUtil.GetToken(user);
        
        return Results.Ok(token);
    }
}