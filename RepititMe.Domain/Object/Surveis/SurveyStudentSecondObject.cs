using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Object.Surveis
{
    public class SurveyStudentSecondObject
    {
        public int TelegramId { get; set; }
        public int OrderId { get; set; }
        public bool? StudentAccept { get; set; }
        public bool? StudentCancel { get; set; }
        public bool? RepitSurveyStudent { get; set; }
        public string? StudentWhy { get; set; }
    }
}
