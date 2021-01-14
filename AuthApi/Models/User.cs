using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Common.MongoDb.V1;

namespace AuthApi.Models
{
    [BsonIgnoreExtraElements]
    public class User : MongoDbEntity
    {
        public string email { get;  set; }
        public string password { get; set; }
    }
}
