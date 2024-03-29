﻿using RepititMe.Domain.Entities.Users;
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
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public DateTime DateTime { get; set; }
        public string? Description { get; set; }
        public int Rating { get; set; }
    }
}
