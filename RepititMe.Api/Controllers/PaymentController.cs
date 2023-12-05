using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RepititMe.Application.Common;
using RepititMe.Application.Services.Orders.Commands;
using RepititMe.Application.Services.Orders.Queries;
using RepititMe.Domain.Object.Orders;
using Yandex.Checkout.V3;

namespace RepititMe.Api.Controllers
{
    [ApiController]
    [EnableCors("enablecorspolicy")]
    public class PaymentController : Controller
    {
        private readonly IPayment _payment;
        public PaymentController(IPayment payment)
        {
            _payment = payment;
        }

        
        [HttpPost("Api/Payment/Create")]
        public async Task<string> CreatePaymentAsync(double value, int orderId)
        {
            string confirmationUrl = await _payment.CreatePayment(value, orderId);

            if (!string.IsNullOrEmpty(confirmationUrl))
            {
                // Редирект на страницу подтверждения оплаты
                return confirmationUrl;
            }
            else
            {
                // Обработка ошибки, например, возврат ошибочного представления
                return "Err";
            }
        }

        [HttpPost("Api/Payment/Check")]
        public async Task<bool> CheckPaymentAsync(int orderId)
        {
            return await _payment.CheckPayment(orderId);
        }

        [HttpGet("Api/Payment/Status/Check")]
        public async Task CheckPaymentStatusAsync(int orderId)
        {
            await _payment.CheckPaymentStatus(orderId);
        }
    }
}
