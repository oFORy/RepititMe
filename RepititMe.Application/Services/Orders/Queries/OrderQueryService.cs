using RepititMe.Application.Services.Orders.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Orders.Queries
{
    public class OrderQueryService : IOrderQueryService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderQueryService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> AcceptOrder(int idOrder)
        {
            return await _orderRepository.AcceptOrder(idOrder);
        }

        public async Task<bool> CancelOrder(int idOrder)
        {
            return await _orderRepository.CancelOrder(idOrder);
        }

        public async Task<bool> RefuseOrder(int idOrder, int user)
        {
            return await _orderRepository.RefuseOrder(idOrder, user);
        }
    }
}
