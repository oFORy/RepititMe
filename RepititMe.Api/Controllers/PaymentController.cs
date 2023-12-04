using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yandex.Checkout.V3;

namespace RepititMe.Api.Controllers
{
    public class PaymentController : Controller
    {
       /* private readonly string shopId = "ваш_shopId";
        private readonly string secretKey = "ваш_secretKey";

        [HttpPost]
        [Route("create")]
        public ActionResult<string> CreatePayment()
        {
            var client = new Client(
                shopId: Environment.GetEnvironmentVariable("Shop_Id"),
                secretKey: Environment.GetEnvironmentVariable("Secret_Key"));

            AsyncClient asyncClient = client.MakeAsync();

            // 1. Создайте платеж и получите ссылку для оплаты
            var newPayment = new NewPayment
            {
                Amount = new Amount { Value = 100.00m, Currency = "RUB" },
                Confirmation = new Confirmation
                {
                    Type = ConfirmationType.Redirect,
                    ReturnUrl = "http://myshop.ru/thankyou"
                }
            };
            Payment payment = client.CreatePayment(newPayment);

            // 2. Перенаправьте пользователя на страницу оплаты
            string url = payment.Confirmation.ConfirmationUrl;
            Response.Redirect(url);

            // 3. Дождитесь получения уведомления
            Message message = Client.ParseMessage(Request.HttpMethod, Request.ContentType, Request.InputStream);
            Payment payment = message?.Object;

            if (message?.Event == Event.PaymentWaitingForCapture && payment.Paid)
            {
                // 4. Подтвердите готовность принять платеж
                _client.CapturePayment(payment.Id);
            }

            return payment.Confirmation.ConfirmationUrl;
        }*/
    }
}
