using RepititMe.Domain.Entities.Data;
using RepititMe.Domain.Object.Sciences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Object.SearchCategory
{
    public class SearchCategoriesObject
    {
        public List<AgeCategory> AgeCategories { get; set; }
        public List<ScienceDto> Sciences { get; set; }
        public List<TeacherStatus> TeacherStatuses { get; set; }
    }
}
