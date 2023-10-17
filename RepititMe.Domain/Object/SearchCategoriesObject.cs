using RepititMe.Domain.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Object
{
    public class SearchCategoriesObject
    {
        public List<AgeСategory> AgeCategories { get; set; }
        public List<LessonTarget> LessonTargets { get; set; }
        public List<Science> Sciences { get; set; }
        public List<TeacherStatus> TeacherStatuses { get; set; }
    }
}
