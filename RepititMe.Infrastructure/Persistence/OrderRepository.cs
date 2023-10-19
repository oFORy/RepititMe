using RepititMe.Application.Services.Orders.Common;
using RepititMe.Domain.Entities.Weights;
using RepititMe.Domain.Object.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Infrastructure.Persistence
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BotDbContext _botDbContext;
        public OrderRepository(BotDbContext context)
        {
            _botDbContext = context;
        }

        public Task<bool> AcceptOrder(int idOrder)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CancelOrder(int idOrder)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> NewOrder(NewOrderObject newOrderObject)
        {
            var newOrder = new Order()
            {
                TeacherId = newOrderObject.TeacherId,
                StudentId = newOrderObject.StudentId,
                Description = newOrderObject.Description,
                IsRefused = false
            };

            _botDbContext.Orders.Add(newOrder);

            return await _botDbContext.SaveChangesAsync() > 0;
        }

        public Task<bool> RefuseOrder(int idOrder)
        {
            throw new NotImplementedException();
        }
    }
}
