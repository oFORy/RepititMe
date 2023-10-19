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
        public Teacher Teachers { get; set; }
        public List<TeacherUseFulUrl>? UsefulLinks { get; set; }
    }
}
