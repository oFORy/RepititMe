using Microsoft.AspNetCore.Http;
using RepititMe.Domain.Entities.Data;
using RepititMe.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Object.Teachers
{
    public class FullTeacherObject
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public IFormFile? Image { get; set; }
        public int? StatusId { get; set; }
        public TeacherStatus? Status { get; set; }
        public int? ScienceId { get; set; }
        public Science? Science { get; set; }
        public int? LessonTargetId { get; set; }
        public LessonTarget? LessonTarget { get; set; }
        public int? AgeСategoryId { get; set; }
        public AgeCategory? AgeСategory { get; set; }
        public int? Experience { get; set; }
        public string? AboutMe { get; set; }
        public int? Price { get; set; }
        public List<IFormFile>? VideoPresentation { get; set; }
        public List<IFormFile>? Certificates { get; set; }
        public double Rating { get; set; }
        public double PaymentRating { get; set; }
    }
}
