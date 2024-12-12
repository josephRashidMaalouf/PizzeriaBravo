using FoodStuffService.Domain.Entities;
using FoodStuffService.Domain.Interfaces;
using FoodStuffService.Domain.Models;
using FoodStuffService.Dtos;
using FoodStuffService.Dtos.Mappers;

namespace FoodStuffService.EndpointExtensions;

public static class FoodStuffEndpoints
{
    public static IEndpointRouteBuilder MapEndPoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/food-stuffs");

        group.MapGet("", GetAll);
        
        group.MapGet("{id}", GetById);
        
        group.MapPost("", Create);
        
        group.MapPut("{id}", Update);
        
        group.MapDelete("{id}", Delete);

        
        return app;
    }

    private static async Task<IResult> GetAll(IFoodStuffService foodStuffService)
    {
        var result = await foodStuffService
            .GetAllAsync();
        
        var dto = result.ToDto();
        
        return result.IsSuccess ? Results.Ok(dto) : Results.BadRequest(dto);
    }

    private static async Task<IResult> GetById(IFoodStuffService foodStuffService, Guid id)
    {
        var result = await foodStuffService.GetByIdAsync(id);
        
        var dto = result.ToDto();
        
        if (result.Code == 404)
        {
            return Results.NotFound(dto);
        }
        
        return result.IsSuccess ? Results.Ok(dto) : Results.BadRequest(dto);
    }

    private static async Task<IResult> Create(IMessageService messageService, FoodStuff foodStuff)
    {
        await messageService.PublishMessageAsync(foodStuff);

        var dto = new ResultDto<string>
        {
            Data = "Message sent",
            IsSuccess = true,
        };
        
        return Results.Ok(dto);
        
        // var result = await foodStuffService.CreateAsync(foodStuff);
        //
        // var dto = result.ToDto();
        //
        // return result.IsSuccess ? Results.Ok(dto) : Results.BadRequest(dto);
    }

    private static async Task<IResult> Update(IFoodStuffService foodStuffService, Guid id, FoodStuff foodStuff)
    {
        var result = await foodStuffService.UpdateAsync(id, foodStuff);
        
        var dto = result.ToDto();
        
        if (result.Code == 404)
        {
            return Results.NotFound(dto);
        }
        
        return result.IsSuccess ? Results.Ok(dto) : Results.BadRequest(dto);
    }

    private static async Task<IResult> Delete(IFoodStuffService foodStuffService, Guid id)
    {
        var result = await foodStuffService.DeleteAsync(id);
        
        var dto = result.ToDto();
        
        if (result.Code == 404)
        {
            return Results.NotFound(dto);
        }
        
        return result.IsSuccess ? Results.Ok(dto) : Results.BadRequest(dto);
    }
}