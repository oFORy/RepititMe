using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RepititMe.Application.Common
{
    public interface IPayment
    {
        Task<string> CreatePayment(double value, int orderId, int countLesson);
        Task<bool> CheckPayment(int orderId);
        Task CheckPaymentStatus(int orderId);
    }

    public class Payment : IPayment
    {
        private readonly IPaymentRepository _repository;
        public Payment(IPaymentRepository paymentRepository)
        {
            _repository = paymentRepository;
        }

        public async Task<bool> CheckPayment(int orderId)
        {
            return await _repository.CheckPayment(orderId);
        }

        public async Task CheckPaymentStatus(int orderId)
        {
            var dataPayment = await _repository.GetPaymentData(orderId);
            if (dataPayment != null)
            {
                string shopId = Environment.GetEnvironmentVariable("Shop_Id");
                string secretKey = Environment.GetEnvironmentVariable("Secret_Key");

                string apiUrl = $"https://api.yookassa.ru/v3/payments/{dataPayment.PaymentId}";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                        "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{shopId}:{secretKey}")));


                    await Task.Run(async () =>
                    {
                        int maxAttempts = 20;
                        int currentAttempt = 0;

                        while (currentAttempt < maxAttempts)
                        {
                            try
                            {
                                var response = await client.GetAsync(apiUrl);

                                if (response.IsSuccessStatusCode)
                                {
                                    var paymentInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
                                    if (paymentInfo["paid"].ToObject<bool>())
                                    {
                                        Console.WriteLine("Платеж оплачен успешно.");
                                        await _repository.ConfirmPayment(orderId, dataPayment.PaymentId);
                                        break;
                                    }
                                    else
                                        Console.WriteLine("Платеж еще не ордакв");
                                }
                                else
                                {
                                    Console.WriteLine($"Ошибка при проверке платежа. Код ошибки: {response.StatusCode}");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error during payment status check: {ex.Message}");
                            }

                            await Task.Delay(30000);
                            currentAttempt++;
                        }

                        Console.WriteLine("Payment status checking completed.");

                        if (currentAttempt >= maxAttempts)
                        {
                            Console.WriteLine("Not paid");
                            await _repository.DeletePayment(orderId, dataPayment.PaymentId);
                        }
                    });
                }
            }
        }

        public async Task<string> CreatePayment(double value, int orderId, int countLesson)
        {
            string shopId = Environment.GetEnvironmentVariable("Shop_Id");
            string secretKey = Environment.GetEnvironmentVariable("Secret_Key");
            string idempotenceKey = Guid.NewGuid().ToString();

            string returnUrl = $"{Environment.GetEnvironmentVariable("ReturnUrl")}/orders/{orderId}?countLesson={countLesson}";

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
                    Console.WriteLine(paymentResponse.id);

                    if (await _repository.Createpayment(orderId, (string)paymentResponse.id, value))
                        return confirmationUrl;
                    return "";
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

