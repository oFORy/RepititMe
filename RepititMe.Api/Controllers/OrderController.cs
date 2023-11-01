using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RepititMe.Application.Services.Orders.Commands;
using RepititMe.Application.Services.Orders.Queries;
using RepititMe.Domain.Entities.Weights;
using RepititMe.Domain.Object.Orders;

namespace RepititMe.Api.Controllers
{
    [ApiController]
    [EnableCors("enablecorspolicy")]
    public class OrderController : Controller
    {
        private readonly IOrderQueryService _orderQueryService;
        private readonly IOrderCommandService _orderCommandService;
        public OrderController(IOrderCommandService orderCommandService, IOrderQueryService orderQueryService)
        {
            _orderCommandService = orderCommandService;
            _orderQueryService = orderQueryService;
        }

        /// <summary>
        /// Создает новую заявку
        /// </summary>
        /// <param name="newOrderObject"></param>
        /// <returns></returns>
        [HttpPost("Api/Order/NewOrder")]
        public async Task<bool> NewOrder([FromBody] NewOrderObject newOrderObject)
        {
            return await _orderCommandService.NewOrder(newOrderObject);
        }

        /// <summary>
        /// Показывает все заявки студента
        /// </summary>
        /// <param name="telegramId"></param>
        /// <returns></returns>
        [HttpGet("Api/Order/ShowAll/Student")]
        public async Task<ShowAllOrdersObject> ShowAllOrdersStudent(int telegramId)
        {
            return await _orderCommandService.ShowAllOrdersStudent(telegramId);
        }

        /// <summary>
        /// Показывает все заявки учителя
        /// </summary>
        /// <param name="telegramId"></param>
        /// <returns></returns>
        [HttpGet("Api/Order/ShowAll/Teacher")]
        public async Task<ShowAllOrdersObject> ShowAllOrdersTeacher(int telegramId)
        {
            return await _orderCommandService.ShowAllOrdersTeacher(telegramId);
        }

        /// <summary>
        /// Прекратить сотрудничетсво
        /// </summary>
        /// <param name="idOrder"></param>
        /// <returns></returns>
        [HttpPut("Api/Order/Refuse")]
        public async Task<bool> RefuseOrder(RefuseOrederObject refuseOrederObject)
        {
            return await _orderQueryService.RefuseOrder(refuseOrederObject);
        }

        /// <summary>
        /// Принять заявку
        /// </summary>
        /// <param name="idOrder"></param>
        /// <returns></returns>
        [HttpPut("Api/Order/Accept")]
        public async Task<bool> AcceptOrder(int idOrder)
        {
            // отправ данных через бота

            return await _orderQueryService.AcceptOrder(idOrder);
        }

        /// <summary>
        /// Отменить заявку
        /// </summary>
        /// <param name="idOrder"></param>
        /// <returns></returns>
        [HttpPut("Api/Order/Cancel")]
        public async Task<bool> CancelOrder(int idOrder)
        {
            return await _orderQueryService.CancelOrder(idOrder);
        }
    }
}
