using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentsApi.Models.Request
{
    public class PaymentModel
    {
        public string token { get; set; }
        public string orderId { get; set; }
    }
}
