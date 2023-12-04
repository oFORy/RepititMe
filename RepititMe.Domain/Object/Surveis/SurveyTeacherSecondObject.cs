using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Object.Surveis
{
    public class SurveyTeacherSecondObject
    {
        public long TelegramId { get; set; }
        public int OrderId { get; set; }
        public bool? TeacherAccept { get; set; }
        public bool? TeacherCancel { get; set; }
        public bool? RepitSurveyTeacher { get; set; }
        public string? TeacherCause { get; set; }
        public string? TeacherSpecify { get; set; }
        public string? TeacherWhy { get; set; }
        public DateTime? DateTimeRepit { get; set; }
    }
}
