using RepititMe.Domain.Entities.Weights;
using RepititMe.Domain.Object.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Orders.Common
{
    public interface IOrderRepository
    {
        Task<bool> NewOrder(NewOrderObject newOrderObject);
        Task<List<Order>> ShowAllOrdersStudent(int telegramId);
        Task<List<Order>> ShowAllOrdersTeacher(int telegramId);
        Task<bool> AcceptOrder(int idOrder);
        Task<bool> RefuseOrder(int idOrder);
        Task<bool> CancelOrder(int idOrder);
    }
}
