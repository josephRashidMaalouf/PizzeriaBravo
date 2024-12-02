using FoodStuffService.Domain.Entities;
using FoodStuffService.Domain.Models;

namespace FoodStuffService.Domain.Interfaces;

public interface ICrud<T> where T : EntityBase
{
    Task<Result<IEnumerable<T>>> GetAllAsync();
    Task<Result<T>> GetByIdAsync(Guid id);
    Task<Result<T>> CreateAsync(T foodStuff);
    Task<Result<T>> UpdateAsync(Guid id, T foodStuff);
    Task<Result<bool>> DeleteAsync(Guid id);
}