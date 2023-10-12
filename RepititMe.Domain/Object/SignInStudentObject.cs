using RepititMe.Domain.Entities;
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
        public string Image { get; set; }
        public List<string> UsefulLinks { get; set; }
        public List<Teacher> Teachers { get; set; }
    }
}
