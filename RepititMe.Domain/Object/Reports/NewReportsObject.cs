using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Object.Reports
{
    public class NewReportsObject
    {
        public int OrderId { get; set; }
        public double Price { get; set; }
        public DateTime DateTimeReport { get; set; }
        public string? Description { get; set; }
    }
}
