using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public int TelegramId { get; set; }
        public string Name { get; set; }
        public string? SecondName { get; set; }
        public string? Image { get; set; }
        public int StatusId { get; set; }
        public TeacherStatus Status { get; set; }
        public int ScienceId { get; set; }
        public Science Science { get; set; }
        public int LessonTargetId { get; set; }
        public LessonTarget LessonTarget { get; set; }
        public int AgeСategoryId { get; set; }
        public AgeСategory AgeСategory { get; set; }
        public string? Experience { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public string? VideoPresentation { get; set; }
        public string? Certificates { get; set; }
        public List<string> UsefulLinks { get; set; }
        public bool Visibility { get; set; }


    }
}
