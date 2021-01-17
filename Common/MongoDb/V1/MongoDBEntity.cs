using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ticket.Common.MongoDb.V1
{
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

        [BsonElement("__v", Order = 1)]
        [BsonRepresentation(BsonType.Int32)]
        public int Version { get; set; }
    }
}