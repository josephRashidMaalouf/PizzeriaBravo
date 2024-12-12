using FoodStuffService.Domain.Entities;
using FoodStuffService.Dtos;

namespace FoodStuffService.Domain.Interfaces;

public interface IMessageService
{
    Task PublishMessageAsync<T>(Message<T> message);
}