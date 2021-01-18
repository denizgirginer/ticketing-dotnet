using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ticket.Common.MongoDb.V1;

namespace TicketsApi.Models
{
    [BsonIgnoreExtraElements]
    public class TicketBase : MongoDbEntity
    {
        public string title { get; set; }
        public decimal price { get; set; }

    }

    [BsonIgnoreExtraElements]
    public class Ticket : TicketBase
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string userId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string orderId { get; set; }
        
    }
}
