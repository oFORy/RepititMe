using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Object.Orders
{
    public class NewOrderObject
    {
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public string? Description { get; set; }
    }
}
