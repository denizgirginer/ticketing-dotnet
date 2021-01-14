using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Ticket.Common.MongoDb.V1;

namespace OrdersApi.Models
{
    [BsonIgnoreExtraElements]
    public class Ticket : MongoDbEntity
    {
        public string title { get; set; }
        public decimal price { get; set; }

    }

}
