﻿using RepititMe.Domain.Entities.Data;
using RepititMe.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
        public bool RefusedStudent { get; set; }
        public bool RefusedTeacher { get; set; }
        public double Commission { get; set; }
        public double PaidCommission { get; set; } = 0;

        public bool NotificationTimeAccept { get; set; } = false;
        public bool NotificationTimeFirstLesson { get; set; } = false;
    }
}
