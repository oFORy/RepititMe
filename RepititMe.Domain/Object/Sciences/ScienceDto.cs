using RepititMe.Domain.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Object.Sciences
{
    public class ScienceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<LessonTargetDto> LessonTargets { get; set; }
    }

    public class LessonTargetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
