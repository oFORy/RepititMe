﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public int TelegramId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public List<string> UsefulLinks { get; set; }
    }
}
