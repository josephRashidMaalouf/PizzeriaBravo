using FoodStuffService.Domain.Entities;
using FoodStuffService.Domain.Interfaces;
using FoodStuffService.Domain.Models;

namespace FoodStuffService.Application.Services;

public class FoodStuffService(IFoodStuffRepository repository) : IFoodStuffService
{
    private readonly IFoodStuffRepository _repository = repository;
    
    public async Task<Result<IEnumerable<FoodStuff>>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Result<FoodStuff>> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Result<FoodStuff>> CreateAsync(FoodStuff foodStuff)
    {
        return await _repository.CreateAsync(foodStuff);
    }

    public async Task<Result<FoodStuff>> UpdateAsync(Guid id, FoodStuff foodStuff)
    {
        return await _repository.UpdateAsync(id, foodStuff);
    }

    public async Task<Result<object>> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }
}