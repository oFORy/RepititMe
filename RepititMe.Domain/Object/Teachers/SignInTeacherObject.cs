using RepititMe.Domain.Entities.Data;
using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Object.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Object.Teachers
{
    public class SignInTeacherObject
    {
        public Teacher TeacherIn { get; set; }
        public List<TeacherUseFulUrl>? UsefulLinks { get; set; }
        public bool SurveyStatusFirst { get; set; }
        public List<OrderSurveyDetailsTeacher> OrdersSurveyFirst { get; set; }

        public bool SurveyStatusSecond { get; set; }
        public List<OrderSurveyDetailsTeacher> OrdersSurveySecond { get; set; }
    }
}
