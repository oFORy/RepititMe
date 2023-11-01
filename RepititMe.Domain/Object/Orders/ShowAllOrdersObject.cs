using RepititMe.Domain.Entities.Weights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Object.Orders
{
    public class ShowAllOrdersObject
    {
        public List<Order> Orders { get; set; }
        public List<int>? CountLesson { get; set; }
    }
}
