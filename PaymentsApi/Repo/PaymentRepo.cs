using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Common.MongoDb.V1;

namespace PaymentsApi.Repo
{
    public class PaymentRepo : MongoDbRepositoryBase<Models.Payment>, IPaymentRepo
    {
        public override string CollectionName => "payments";

        public PaymentRepo(IOptions<MongoDbSettings> options) : base(options)
        {
        }
    }
}
