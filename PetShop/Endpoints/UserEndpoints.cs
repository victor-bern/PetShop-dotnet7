using Microsoft.AspNetCore.Authorization;
using PetShop.Domain.Entities;
using PetShop.Domain.Repositories;
using PetShop.Dto;
using PetShop.Extensions;
using PetShop.Utils;
using PetShop.Validators;

namespace PetShop.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("api/v1/users", GetAllUsers).WithName("GetUsers").WithOpenApi();
        routeBuilder.MapGet("api/v1/users/{id:int}", GetUserById).WithName("GetUserById").WithOpenApi();
        routeBuilder.MapPost("api/v1/users", SaveUser).WithName("SaveUser").WithOpenApi();
        routeBuilder.MapPut("api/v1/users/update/{id:int}", EditUser).WithName("EditUser").WithOpenApi();
        routeBuilder.MapDelete("api/v1/users/delete/{id:int}", DeleteUser).WithName("DeleteUser").WithOpenApi();
    }


    [Authorize(Roles = "Admin")]
    private static async Task<IResult> GetAllUsers(IUserRepository userRepository)
    {
        var users = await userRepository.GetAll();
        return Results.Ok(users);
    }


    [Authorize(Roles = "Admin")]
    private static async Task<IResult> GetUserById(int id, IUserRepository userRepository)
    {
        var user = await userRepository.GetById(id);
        return user == null ? Results.NoContent() : Results.Ok(user);
    }

    [AllowAnonymous]
    private static async Task<IResult> SaveUser(User model, IUserRepository userRepository)
    {
        try
        {
            var validator = new UserValidator();
            var result = await validator.ValidateAsync(model);

            if (!result.IsValid)
                return Results.BadRequest(new
                {
                    Errors = result.GetErrors()
                });

            var emailExists = await userRepository.GetByEmail(model.Email);

            if (emailExists != null)
                return Results.BadRequest(new
                {
                    Error = "Email already exists"
                });

            var passwordHashed = await AuthUtil.HashPass(model.Password);

            model.Password = passwordHashed;

            var user = await userRepository.SaveUser(model);
            user.Password = "";
            return Results.Ok(user);
        }
        catch (Exception)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [Authorize]
    private static async Task<IResult> EditUser(int id, UpdateUserDto model, IUserRepository userRepository)
    {
        try
        {
            var validator = new UpdateUserDtoValidator();
            var validatorResult = await validator.ValidateAsync(model);

            if (!validatorResult.IsValid)
                return Results.BadRequest(new
                {
                    Errors = validatorResult.GetErrors()
                });

            var user = await userRepository.GetById(id);
            if (user == null) return Results.NoContent();

            user.Name = model.Name;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.BirthDate = model.BirthDate;

            var userResult = await userRepository.UpdateUser(user);

            return Results.Ok(userResult);
        }
        catch (Exception)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [Authorize(Roles = "Admin")]
    private static async Task<IResult> DeleteUser(int id, IUserRepository userRepository)
    {
        try
        {
            var user = await userRepository.GetById(id);
            if (user == null) return Results.NoContent();

            await userRepository.DeleteUser(user);

            return Results.NotFound();
        }
        catch (Exception)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}