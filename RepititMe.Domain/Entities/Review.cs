using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int IdTeacher { get; set; }
        public int IdStudent { get; set; }
        public string? Description { get; set; }
    }
}
