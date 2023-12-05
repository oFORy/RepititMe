using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Common
{
    public interface IPayment
    {
        Task<string> CreatePayment(double value, string description);
        Task CheckPayment(string paymentId, double value, long telegramId);
    }

    public class Payment : IPayment
    {
        private readonly IPaymentRepository _repository;
        public Payment(IPaymentRepository paymentRepository)
        {
            _repository = paymentRepository;
        }

        public async Task CheckPayment(string paymentId, double value, long telegramId)
        {
            string shopId = Environment.GetEnvironmentVariable("Shop_Id");
            string secretKey = Environment.GetEnvironmentVariable("Secret_Key");

            string apiUrl = $"https://api.yookassa.ru/v3/payments/{paymentId}";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{shopId}:{secretKey}")));

                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var paymentInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
                    Console.WriteLine($"Информация о платеже: {paymentInfo}");
                    if (paymentInfo["paid"].ToObject<bool>())
                    {
                        Console.WriteLine("Платеж оплачен успешно.");
                        await _repository.ConfirmPayment(telegramId, value);
                    }
                    else
                    {
                        Console.WriteLine("Платеж не оплачен.");
                    }
                }
                else
                {
                    Console.WriteLine($"Ошибка при проверке платежа. Код ошибки: {response.StatusCode}");
                }
            }
        }

        public async Task<string> CreatePayment(double value, string descriptionData)
        {
            string shopId = Environment.GetEnvironmentVariable("Shop_Id");
            string secretKey = Environment.GetEnvironmentVariable("Secret_Key");
            string idempotenceKey = Guid.NewGuid().ToString();
            string returnUrl = "https://yookassa.ru/developers/payment-acceptance/getting-started/quick-start?codeLang=bash";

            string currency = "RUB";

            var paymentRequest = new
            {
                amount = new
                {
                    value = value.ToString("0.00"),
                    currency = currency
                },
                capture = true,
                confirmation = new
                {
                    type = "redirect",
                    return_url = returnUrl
                },
                description = descriptionData
            };

            string apiUrl = "https://api.yookassa.ru/v3/payments";
            string paymentJson = Newtonsoft.Json.JsonConvert.SerializeObject(paymentRequest);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{shopId}:{secretKey}")));

                client.DefaultRequestHeaders.Add("Idempotence-Key", idempotenceKey);

                var content = new StringContent(paymentJson, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var paymentResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
                    string confirmationUrl = paymentResponse.confirmation.confirmation_url;
                    Console.WriteLine($"Платеж создан. Перенаправьте пользователя по следующей ссылке для оплаты: {confirmationUrl}");
                    return confirmationUrl;
                }
                else
                {
                    Console.WriteLine($"Ошибка при создании платежа. Код ошибки: {response.StatusCode}");
                    return "";
                }
            }
        }
    }
}
