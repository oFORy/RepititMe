using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Object.Reviews
{
    public class ReviewObject
    {
        public long TelegramIdTeacher { get; set; }
        public long TelegramIdStudent { get; set; }
        public string? Description { get; set; }
        public int Rating { get; set; }
    }
}
