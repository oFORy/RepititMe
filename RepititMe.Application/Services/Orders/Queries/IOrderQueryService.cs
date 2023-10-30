using RepititMe.Domain.Object.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Orders.Queries
{
    public interface IOrderQueryService
    {
        Task<bool> AcceptOrder(int idOrder);
        Task<bool> RefuseOrder(RefuseOrederObject refuseOrederObject);
        Task<bool> CancelOrder(int idOrder);
    }
}
