using FoodStuffService.Domain.Entities;
using FoodStuffService.Domain.Interfaces;

namespace FoodStuffService.EndpointExtensions;

public static class FoodStuffEndpoints
{
    public static IEndpointRouteBuilder MapEndPoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/food-stuffs");

        group.MapGet("", GetAll).WithName("GetAll");
        
        group.MapGet("{id}", GetById);
        
        group.MapPost("{id}", Create);
        
        group.MapPut("", Update);
        
        group.MapDelete("{id}", Delete);
        
        return app;
    }

    private static async Task<IResult> GetAll(IFoodStuffService foodStuffService)
    {
        var result = await foodStuffService.GetAllAsync();
        
        return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
    }

    private static async Task<IResult> GetById(IFoodStuffService foodStuffService, Guid id)
    {
        var result = await foodStuffService.GetByIdAsync(id);

        if (result.Code == 404)
        {
            return Results.NotFound(result);
        }
        
        return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
    }

    private static async Task<IResult> Create(IFoodStuffService foodStuffService, FoodStuff foodStuff)
    {
        var result = await foodStuffService.CreateAsync(foodStuff);
        
        return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
    }

    private static async Task<IResult> Update(IFoodStuffService foodStuffService, Guid id, FoodStuff foodStuff)
    {
        var result = await foodStuffService.UpdateAsync(id, foodStuff);
        
        if (result.Code == 404)
        {
            return Results.NotFound(result);
        }
        
        return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
    }

    private static async Task<IResult> Delete(IFoodStuffService foodStuffService, Guid id)
    {
        var result = await foodStuffService.DeleteAsync(id);
        
        if (result.Code == 404)
        {
            return Results.NotFound(result);
        }
        
        return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
    }
}