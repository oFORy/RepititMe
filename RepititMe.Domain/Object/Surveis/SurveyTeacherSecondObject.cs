using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Object.Surveis
{
    public class SurveyTeacherSecondObject
    {
        public int TelegramId { get; set; }
        public int OrderId { get; set; }
        public bool? TeacherAccept { get; set; }
        public bool? TeacherCancel { get; set; }
        public bool? RepitSurveyTeacher { get; set; }
    }
}
