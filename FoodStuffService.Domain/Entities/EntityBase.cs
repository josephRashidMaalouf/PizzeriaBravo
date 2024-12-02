using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FoodStuffService.Domain.Entities;

public class EntityBase
{
    [BsonId]
    [BsonRepresentation(BsonType.String)] 
    public Guid Id { get; set; } = Guid.NewGuid();
}