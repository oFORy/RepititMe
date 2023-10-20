using RepititMe.Domain.Entities.Data;
using RepititMe.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Entities.Weights
{
    public class Order
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public DateTime? DateTimeAccept { get; set; }
        public DateTime? DateTimeFirstLesson { get; set; }
        public string? Description { get; set; }
        public bool IsRefused { get; set; }
        public bool IsPaid { get; set; }
    }
}
