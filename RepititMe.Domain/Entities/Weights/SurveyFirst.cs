using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Entities.Weights
{
    public class SurveyFirst
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public bool StudentAnswer { get; set; }
        public long TelegramIdStudent { get; set; }
        public bool? StudentAccept { get; set; }
        public double? StudentPrice { get; set; }
        public string? StudentWhy { get; set; }

        public bool TeacherAnswer { get; set; }
        public long TelegramIdTeacher { get; set; }
        public bool? TeacherAccept { get; set; }
        public double? TeacherPrice { get; set; }
        public string? TeacherCause { get; set; }
        public DateTime? RepitSurveyTeacher { get; set; }
        public string? TeacherSpecify { get; set; }
        public string? TeacherWhy { get; set; }
    }
}
