namespace FoodStuffService.Dtos;

public class ResultDto<T> where T : class
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
}