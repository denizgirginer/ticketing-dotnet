using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Common.MongoDb.V1;

namespace PaymentsApi.Models
{
    [BsonIgnoreExtraElements]
    public class Payment: MongoDbEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string userId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string orderId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string stripeId { get; set; }
    }
}
