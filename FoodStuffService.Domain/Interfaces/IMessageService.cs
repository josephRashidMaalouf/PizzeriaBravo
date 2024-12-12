using FoodStuffService.Domain.Entities;

namespace FoodStuffService.Domain.Interfaces;

public interface IMessageService
{
    Task PublishMessageAsync(EntityBase message);
}