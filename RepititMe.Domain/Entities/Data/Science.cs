using RepititMe.Domain.Entities.Weights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RepititMe.Domain.Entities.Data
{
    public class Science
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ScienceLessonTarget> ScienceLessonTargets { get; set; }
    }
}
