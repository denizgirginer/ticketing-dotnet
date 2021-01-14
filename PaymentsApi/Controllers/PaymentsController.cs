﻿using Microsoft.AspNetCore.Mvc;
using PaymentsApi.Models.Request;
using PaymentsApi.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var found = await _orderRepo.GetByIdAsync(payment.orderId);

            if(found==null)
            {
                throw new Exception("Not Found");
            }

            if(found.userId!=GetUserId())
            {
                throw new Exception("Not Authorized");
            }

            string stripeId = "";
            //TODO stribe charge
            /*
             const charge = await stripe.charges.create({
                currency: 'usd',
                amount: order.price * 100,
                source: token
            })
             */

            var newPayment = new Models.Payment() { 
                stripeId=stripeId,
                orderId=found.Id
            };
            await _paymentRepo.AddAsync(newPayment);

            //TODO publish event PaymentCreated

            return Ok(new {
                id= newPayment.Id
            });
        }

        private string GetUserId()
        {
            var claim = HttpContext.User.Claims.SingleOrDefault(x => x.Type == "userId");

            return claim?.Value;
        }
    }
}