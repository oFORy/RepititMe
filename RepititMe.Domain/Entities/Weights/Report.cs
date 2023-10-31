using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Entities.Weights
{
    public class Report
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public DateTime DateTime { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
