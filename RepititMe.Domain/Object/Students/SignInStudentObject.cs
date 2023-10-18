using RepititMe.Domain.Entities;
using RepititMe.Domain.Entities.Data;
using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Object.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Object.Students
{
    public class SignInStudentObject
    {
        public string Name { get; set; }
        public List<StudentUseFulUrl>? UsefulLinks { get; set; }
        public List<BriefTeacher>? Teachers { get; set; }
    }
}
