﻿using RepititMe.Domain.Entities.Data;
using RepititMe.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Object.Teachers
{
    public class BriefTeacherObject
    {
        public User User { get; set; }
        public string? Image { get; set; }
        public TeacherStatus Status { get; set; }
        public Science Science { get; set; }
        public List<LessonTarget> LessonTarget { get; set; }
        public List<AgeCategory> AgeCategory { get; set; }
        public int? Experience { get; set; }
        public string? AboutMe { get; set; }
        public int? Price { get; set; }
        public double Rating { get; set; }
    }
}
