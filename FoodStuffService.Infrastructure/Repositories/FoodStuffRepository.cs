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
            result.Code = 200;
        }
        catch (Exception ex)
        {
            result.Code = 500;
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
            var foodStuff = await _collection.Find(filter).FirstOrDefaultAsync();
            
            if (foodStuff is null)
            {
                result.IsSuccess = false;
                result.Message = $"No entity with id: {id} found";
                result.Code = 404;
                
                return result;
            }
            
            result.Code = 200;
            result.IsSuccess = true;
            result.Data = foodStuff;
        }
        catch (Exception ex)
        {
            result.Code = 500;
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
            result.Code = 200;
        }
        catch (Exception e)
        {
            result.Code = 500;
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

            var updateResult = await _collection.ReplaceOneAsync(filter, foodStuff, options);
            
            if (updateResult.MatchedCount == 0)
            {
                result.IsSuccess = false;
                result.Message = $"No entity with id: {id} found";
                result.Code = 404;
                
                return result;
            }
            
            result.Code = 200;
            result.IsSuccess = true;
            result.Data = foodStuff;
        }
        catch (Exception ex)
        {
            result.Code = 500;
            result.IsSuccess = false;
            result.Message = ex.Message;
        }
        
        return result;
    }

    public async Task<Result<object>> DeleteAsync(Guid id)
    {
        var result = new Result<object>();

        try
        {
            var filter = Builders<FoodStuff>.Filter.Eq(x => x.Id, id);
            var deleteResult = await _collection.DeleteOneAsync(filter);

            if (deleteResult.DeletedCount == 0)
            {
                result.IsSuccess = false;
                result.Message = $"No entity with id: {id} found";
                result.Code = 404;
                
                return result;
            }
            
            result.Code = 200;
            result.IsSuccess = true;
        }
        catch(Exception ex)
        {
            result.Code = 500;
            result.IsSuccess = false;
            result.Message = ex.Message;
        }
        
        return result;
    }
}