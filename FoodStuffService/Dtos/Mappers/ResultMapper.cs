using FoodStuffService.Domain.Entities;
using FoodStuffService.Domain.Models;

namespace FoodStuffService.Dtos.Mappers;

public static class ResultMapper
{
    public static ResultDto<T> ToDto<T>(this Result<T> result) 
    {
        return new ResultDto<T>
        {
            Data = result.Data,
            Message = result.Message,
            IsSuccess = result.IsSuccess,
        };
    }
    
}