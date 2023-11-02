using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Object.Students
{
    public class ShowTeachersFilterObject
    {
        public int? ScienceId { get; set; }
        public double? PriceLow { get; set; }
        public double? PriceHigh { get; set; }
        public int? TeacherStatusId { get; set; }
        public int? LessonTargetId { get; set; }
        public List<long> WasTeachers { get; set; }

    }
}
