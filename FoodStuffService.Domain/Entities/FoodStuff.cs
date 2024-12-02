namespace FoodStuffService.Domain.Entities;

public class FoodStuff : EntityBase
{
    public required string Name { get; set; }
    public required int Price { get; set; }
}