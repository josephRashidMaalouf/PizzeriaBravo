namespace FoodStuffService.Dtos;

public class Message<T>
{
    public required string MethodInfo { get; set; }
    public required T Data {get; set;}
}