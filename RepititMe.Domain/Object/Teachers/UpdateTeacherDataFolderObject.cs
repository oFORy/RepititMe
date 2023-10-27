using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Object.Teachers
{
    public class UpdateTeacherDataFolderObject
    {
        public int UserId { get; set; }
        public string? Image { get; set; }
        public string? VideoPresentation { get; set; }
        public List<string>? Certificates { get; set; }
    }
}
