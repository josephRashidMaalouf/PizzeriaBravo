using FoodStuffService.Domain.Interfaces;

namespace FoodStuffService.EndpointExtensions;

public static class FoodStuffs
{
    public static IEndpointRouteBuilder MapEndPoints(this IEndpointRouteBuilder app)
    {
        app.MapGroup("api/food-stuffs");

        app.MapGet("", GetAll)
            .WithName("FoodStuffs");
        
        return app;
    }

    private static async Task<IResult> GetAll(IFoodStuffService foodStuffService)
    {
        
        
    }
}