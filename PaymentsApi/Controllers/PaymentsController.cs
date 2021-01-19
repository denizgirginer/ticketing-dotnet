using Microsoft.AspNetCore.Mvc;
using PaymentsApi.Models.Request;
using PaymentsApi.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Common.EventBus;
using Ticket.Common.Events;
using Ticket.Common.Helpers;

namespace PaymentsApi.Controllers
{
    [ApiController]
    public class PaymentsController:ControllerBase
    {
        IOrderRepo _orderRepo;
        IPaymentRepo _paymentRepo;
        public PaymentsController(IOrderRepo orderRepo, IPaymentRepo paymentRepo)
        {
            _orderRepo = orderRepo;
            _paymentRepo = paymentRepo;
        }

        [HttpPost]
        [Route("/")]
        public async Task<IActionResult> NewPaymnet(PaymentModel payment)
        {
            var order = await _orderRepo.GetByIdAsync(payment.orderId);

            if(order==null)
            {
                throw new Exception("Not Found");
            }

            if(order.userId!=SessionHelper.GetUserId())
            {
                throw new Exception("Not Authorized");
            }

            string stripeId = Guid.NewGuid().ToString();
            //TODO stripe charge
            /*
             const charge = await stripe.charges.create({
                currency: 'usd',
                amount: order.price * 100,
                source: token
            })
             */

            var newPayment = new Models.Payment() { 
                stripeId=stripeId,
                orderId=order.id
            };
            await _paymentRepo.AddAsync(newPayment);

            //publish PaymentCreated
            var evt = new PaymentCreatedEvent();
            evt.Data.orderId = order.id;
            evt.Data.stripeId = stripeId;
            await evt.Publish();

            return Ok(new {
                id= newPayment.id
            });
        }
    }
}
