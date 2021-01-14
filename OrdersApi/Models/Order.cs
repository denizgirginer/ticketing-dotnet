using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Common.MongoDb.V1;

namespace OrdersApi.Models
{
    [BsonIgnoreExtraElements]
    public class Order: MongoDbEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string userId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string ticketId { get; set; }
        [BsonIgnore]
        public Ticket ticket { get; set; }
        public OrderStatus status { get; set; } = OrderStatus.Created;
        public DateTime expiresAt { get; set; }
    }

    public enum OrderStatus
    {
        Created,
        Cancelled,
        AwaitingPayment,
        Complete
    }

}

