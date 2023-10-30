using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Object.Orders
{
    public class RefuseOrederObject
    {
        public int IdOrder { get; set; }
        public int User { get; set; }
        public string? DescriptionRefuse { get; set; }
    }
}
