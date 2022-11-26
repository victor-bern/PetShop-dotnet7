using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PetShop.Domain.Entities;
using PetShop.Domain.Repositories;
using PetShop.Extensions;
using PetShop.Validators;

namespace PetShop.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("api/v1/products", GetAll);
        routeBuilder.MapGet("api/v1/products/{id:int}", GetById);
        routeBuilder.MapGet("api/v1/products/byfilter", GetByFilter);
        routeBuilder.MapPost("api/v1/products", SaveProduct);
        routeBuilder.MapPut("api/v1/products/update/{id:int}", UpdateProduct);
        routeBuilder.MapDelete("api/v1/products/delete/{id:int}", DeleteProduct);
    }


    [Authorize]
    private static async Task<IResult> GetAll(IProductRepository productRepository)
    {
        var products = await productRepository.GetAll();
        return Results.Ok(products);
    }

    [Authorize]
    private static async Task<IResult> GetById(int id, IProductRepository productRepository)
    {
        var product = await productRepository.GetById(id);
        return product == null ? Results.NotFound() : Results.Ok(product);
    }

    [Authorize]
    private static async Task<IResult> GetByFilter([FromQuery] string filter, [FromQuery]decimal value, IProductRepository productRepository)
    {
        return Results.Ok(await productRepository.GetProductByPriceWithFilter(value, filter));
    }

    [Authorize(Roles = "Admin")]
    private static async Task<IResult> SaveProduct(Product model, IProductRepository productRepository)
    {
        try
        {
            var result = await new ProductValidator().ValidateAsync(model);
            if (!result.IsValid)
                return Results.BadRequest(new
                {
                    Errors = result.GetErrors()
                });

            var product = await productRepository.SaveProduct(model);
            return Results.Ok(product);
        }
        catch (Exception)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [Authorize(Roles = "Admin")]
    private static async Task<IResult> UpdateProduct(int id, Product model, IProductRepository productRepository)
    {
        try
        {
            var product = await productRepository.GetById(id);
            if (product == null) return Results.NotFound();
            
            var result = await new ProductValidator().ValidateAsync(model);
            if (!result.IsValid)
                return Results.BadRequest(new
                {
                    Errors = result.GetErrors()
                });
            product.Title = model.Title;
            product.Price = model.Price;
            
            var productUpdated = await productRepository.UpdateProduct(product);
            return Results.Ok(productUpdated);
        }
        catch (Exception)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);

        }
    }

    [Authorize(Roles = "Admin")]
    private static async Task<IResult> DeleteProduct(int id, IProductRepository productRepository)
    {
        try
        {
            var product = await productRepository.GetById(id);
            if (product == null) return Results.NotFound();

            await productRepository.DeleteProduct(product);
            return Results.NoContent();
        }
        catch (Exception)
        {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);

        }
    }

}