using Newtonsoft.Json;
using RepititMe.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Entities.Data
{
    public class TeacherAgeCategory
    {
        public int TeacherId { get; set; }
        [JsonIgnore]
        public Teacher Teacher { get; set; }

        public int AgeCategoryId { get; set; }
        public AgeCategory AgeCategory { get; set; }
    }
}
