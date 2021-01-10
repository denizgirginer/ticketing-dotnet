using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace API.Models.Abstract {
    public abstract class MongoDbEntity : IEntity<string>
    {
        [BsonId()]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement(Order = 0)]
        public string Id {get; set;}

        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonElement(Order = 101)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
    }
}