using RepititMe.Domain.Entities.Data;
using RepititMe.Domain.Entities.Users;
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
        public bool SurveyStatus { get; set; }
        public List<int> OrdersSurvey { get; set; }
    }
}
