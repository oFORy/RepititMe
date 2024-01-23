using RepititMe.Domain.Entities.Weights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Object.Admins
{
    public class ShowAllOrdersObjectAdmin
    {
        public bool Status { get; set; }
        public List<Order>? Orders { get; set; }
        public List<(int, int)>? Count { get; set; }
    }
}
