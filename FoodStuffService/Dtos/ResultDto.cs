namespace FoodStuffService.Dtos;

public class ResultDto<T> 
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
}