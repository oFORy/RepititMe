using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Object.Reviews
{
    public class ReviewObject
    {
        public int TelegramIdTeacher { get; set; }
        public int TelegramIdStudent { get; set; }
        public string? Description { get; set; }
        public int Rating { get; set; }
    }
}
