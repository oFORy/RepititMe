using RepititMe.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Object.Admins
{
    public class ShowAllStudentsObject
    {
        public bool Status { get; set; }
        public List<Student> Students { get; set; }
    }
}
