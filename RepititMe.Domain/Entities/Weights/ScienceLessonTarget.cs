using RepititMe.Domain.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Entities.Weights
{
    public class ScienceLessonTarget
    {
        public int ScienceId { get; set; }
        public Science Science { get; set; }

        public int LessonTargetId { get; set; }
        public LessonTarget LessonTarget { get; set; }
    }
}
