using RepititMe.Domain.Entities.Weights;
using RepititMe.Domain.Object.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Orders.Commands
{
    public interface IOrderCommandService
    {
        Task<bool> NewOrder(NewOrderObject newOrderObject);
        Task<ShowAllOrdersObject> ShowAllOrdersStudent(long telegramId);
        Task<ShowAllOrdersObject> ShowAllOrdersTeacher(long telegramId);
    }
}
