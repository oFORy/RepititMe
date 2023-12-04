using RepititMe.Domain.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Object.SearchCategory
{
    public class SearchCategoriesResultObject
    {
        public int ScienceId { get; set; }
        public int LessonTargetId { get; set; }
        public int AgeCategoryId { get; set; }
        public List<int> StatusId { get; set; }
        public int LowPrice { get; set; }
        public int HighPrice { get; set; }
    }
}
