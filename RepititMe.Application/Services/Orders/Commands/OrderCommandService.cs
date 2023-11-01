using RepititMe.Application.Services.Orders.Common;
using RepititMe.Domain.Entities.Weights;
using RepititMe.Domain.Object.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Orders.Commands
{
    public class OrderCommandService : IOrderCommandService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderCommandService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> NewOrder(NewOrderObject newOrderObject)
        {
            return await _orderRepository.NewOrder(newOrderObject);
        }

        public async Task<ShowAllOrdersObject> ShowAllOrdersStudent(int telegramId)
        {
            return await _orderRepository.ShowAllOrdersStudent(telegramId);
        }

        public async Task<ShowAllOrdersObject> ShowAllOrdersTeacher(int telegramId)
        {
            return await _orderRepository.ShowAllOrdersTeacher(telegramId);
        }
    }
}
