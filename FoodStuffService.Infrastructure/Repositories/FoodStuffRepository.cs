using FoodStuffService.Domain.Entities;
using FoodStuffService.Domain.Interfaces;
using FoodStuffService.Domain.Models;
using MongoDB.Driver;

namespace FoodStuffService.Infrastructure.Repositories;

public class FoodStuffRepository : IFoodStuffRepository
{
    private readonly IMongoCollection<FoodStuff> _collection;

    public FoodStuffRepository(string databaseName, string collectionName, string connectionString)
    {
        var client = new MongoClient(connectionString);
        var db = client.GetDatabase(databaseName);
        _collection = db.GetCollection<FoodStuff>(collectionName);
    }


    public async Task<Result<IEnumerable<FoodStuff>>> GetAllAsync()
    {
        var result = new Result<IEnumerable<FoodStuff>>();

        try
        {
            var filter = Builders<FoodStuff>.Filter.Empty;
            var foodStuffs = await _collection.FindAsync(filter);
            result.Data = await foodStuffs.ToListAsync();
            result.IsSuccess = true;
        }
        catch (Exception ex)
        {
            result.IsSuccess = false;
            result.Message = ex.Message;
        }
        
        return result;
    }

    public async Task<Result<FoodStuff>> GetByIdAsync(Guid id)
    {
        var result = new Result<FoodStuff>();

        try
        {
            var filter = Builders<FoodStuff>.Filter.Eq(x => x.Id, id);
            var foodStuff = await _collection.Find(filter).FirstAsync();
            result.IsSuccess = true;
            result.Data = foodStuff;
        }
        catch (Exception ex)
        {
            result.IsSuccess = false;
            result.Message = ex.Message;
        }
        
        return result;
    }

    public async Task<Result<FoodStuff>> CreateAsync(FoodStuff foodStuff)
    {
        var result = new Result<FoodStuff>();

        try
        {
            await _collection.InsertOneAsync(foodStuff);
            result.IsSuccess = true;
            result.Data = foodStuff;
        }
        catch (Exception e)
        {
           result.IsSuccess = false;
           result.Message = e.Message;
        }
        
        return result;
    }

    public async Task<Result<FoodStuff>> UpdateAsync(Guid id, FoodStuff foodStuff)
    {
        var result = new Result<FoodStuff>();

        try
        {
            var filter = Builders<FoodStuff>.Filter.Eq(x => x.Id, id);
            var options = new ReplaceOptions { IsUpsert = true };

            await _collection.ReplaceOneAsync(filter, foodStuff, options);

            result.IsSuccess = true;
            result.Data = foodStuff;
        }
        catch (Exception ex)
        {
            result.IsSuccess = false;
            result.Message = ex.Message;
        }
        
        return result;
    }

    public async Task<Result<bool>> DeleteAsync(Guid id)
    {
        var result = new Result<bool>();

        try
        {
            var filter = Builders<FoodStuff>.Filter.Eq(x => x.Id, id);
            await _collection.DeleteOneAsync(filter);

            result.IsSuccess = true;
            result.Data = true;
        }
        catch(Exception ex)
        {
            result.IsSuccess = false;
            result.Message = "Error deleting food stuff";
        }
        
        return result;
    }
}