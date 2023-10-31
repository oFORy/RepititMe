using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Entities.Weights
{
    public class SurveySecond
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }


        public bool StudentAnswer { get; set; }
        public int TelegramIdStudent { get; set; }
        public bool? StudentAccept { get; set; }

        public bool? StudentWannaNext { get; set; }
        public bool? StudentCancel { get; set; }

        public string? StudentWhy { get; set; }
        public DateTime? RepitSurveyStudent { get; set; }



        public bool TeacherAnswer { get; set; }
        public int TelegramIdTeacher { get; set; }
        public bool? TeacherAccept { get; set; }

        public bool? TeacherWannaNext { get; set; }
        public bool? TeacherCancel { get; set; }

        public string? TeacherCause { get; set; }
        public string? TeacherSpecify { get; set; }
        public string? TeacherWhy { get; set; }
        public DateTime? RepitSurveyTeacher { get; set; }
    }
}
