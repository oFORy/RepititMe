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
        Task<bool> AcceptOrder(int idOrder);
        Task<bool> RefuseOrder(int idOrder);
        Task<bool> CancelOrder(int idOrder);
    }
}
