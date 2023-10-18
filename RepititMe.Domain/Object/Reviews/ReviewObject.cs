using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Object.Reviews
{
    public class ReviewObject
    {
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public DateTime DateTime { get; set; }
        public string? Description { get; set; }
        public int Rating { get; set; }
    }
}
