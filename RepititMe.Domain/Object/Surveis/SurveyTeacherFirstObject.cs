using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Object.Surveis
{
    public class SurveyTeacherFirstObject
    {
        public int TelegramId { get; set; }
        public int OrderId { get; set; }
        public bool TeacherAccept { get; set; }
        public DateTime? DateTimeFirstLesson { get; set; }
        public int? TeacherPrice { get; set; }
        public string? TeacherCause { get; set; }
        public string? TeacherSpecify { get; set; }
        public string? TeacherWhy { get; set; }
    }
}
