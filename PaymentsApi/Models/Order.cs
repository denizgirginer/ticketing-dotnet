using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ticket.Common.Events;
using Ticket.Common.MongoDb.V1;

namespace PaymentsApi.Models
{
    [BsonIgnoreExtraElements]
    public class Order: MongoDbEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string userId { get; set; }
        public OrderStatus status { get; set; } = OrderStatus.Created;
        public decimal price { get; set; }
    }


}

