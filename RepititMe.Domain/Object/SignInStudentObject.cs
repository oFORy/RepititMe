using RepititMe.Domain.Entities;
using RepititMe.Domain.Entities.Data;
using RepititMe.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Object
{
    public class SignInStudentObject
    {
        public string Name { get; set; }
        public List<StudentUseFulUrl>? UsefulLinks { get; set; }
        public List<Teacher>? Teachers { get; set; }
    }
}
