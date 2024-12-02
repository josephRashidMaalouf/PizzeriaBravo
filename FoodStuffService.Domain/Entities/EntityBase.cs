using MongoDB.Bson.Serialization.Attributes;

namespace FoodStuffService.Domain.Entities;

public class EntityBase
{
    [BsonId]
    public Guid Id { get; set; } = Guid.NewGuid();
}