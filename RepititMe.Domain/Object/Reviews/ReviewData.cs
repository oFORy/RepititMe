using RepititMe.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Object.Reviews
{
    public class ReviewData
    { 
        public int Id { get; set; }
        public Student Student { get; set; }
        public DateTime DateTime { get; set; }
        public string? Description { get; set; }
        public int Rating { get; set; }
    }
}
