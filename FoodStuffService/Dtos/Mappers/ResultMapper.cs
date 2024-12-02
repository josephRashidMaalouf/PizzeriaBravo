using FoodStuffService.Domain.Entities;
using FoodStuffService.Domain.Models;

namespace FoodStuffService.Dtos.Mappers;

public static class ResultMapper
{
    public static ResultDto<FoodStuff> ToDto(this Result<FoodStuff> result)
    {
        return new ResultDto<FoodStuff>
        {
            Data = result.Data,
            Message = result.Message,
            IsSuccess = result.IsSuccess,
        };
    }
}