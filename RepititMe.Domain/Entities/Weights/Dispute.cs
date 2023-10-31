using RepititMe.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Entities.Weights
{
    public class Dispute
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }


        public bool? AcceptFromTeacher { get; set; }
        public double? PriceTeacher { get; set; }
        public string? DataFromTeacher { get; set; }

        public bool? AcceptFromStudent { get; set; }
        public double? PriceStudent { get; set; }
        public string? DataFromStudent { get; set; }
    }
}
