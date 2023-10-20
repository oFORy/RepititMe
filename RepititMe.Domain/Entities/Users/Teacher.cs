using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepititMe.Domain.Entities.Data;

namespace RepititMe.Domain.Entities.Users
{
    public class Teacher
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string? Image { get; set; }
        public int? StatusId { get; set; }
        public TeacherStatus? Status { get; set; }
        public int? ScienceId { get; set; }
        public Science? Science { get; set; }
        public int? LessonTargetId { get; set; }
        public LessonTarget? LessonTarget { get; set; }
        public int? AgeСategoryId { get; set; }
        public AgeCategory? AgeCategory { get; set; }
        public int? Experience { get; set; }
        public string? AboutMe { get; set; }
        public int? Price { get; set; }
        public List<string>? VideoPresentation { get; set; }
        public List<string>? Certificates { get; set; }
        public bool Visibility { get; set; }
        public bool Block { get; set; }
        public double Rating { get; set; }
        public double PaymentRating { get; set; }
    }
}
