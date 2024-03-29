﻿using RepititMe.Domain.Entities;
using RepititMe.Domain.Entities.Data;
using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Object.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Object.Students
{
    public class SignInStudentObject
    {
        public string Name { get; set; }
        public List<StudentUseFulUrl>? UsefulLinks { get; set; }

        //public List<BriefTeacherObject>? Teachers { get; set; }
        public bool Blocked { get; set; }

        public bool SurveyStatusFirst { get; set; }
        public List<OrderSurveyDetailsStudent> OrdersSurveyFirst { get; set; }

        public bool SurveyStatusSecond { get; set; }
        public List<OrderSurveyDetailsStudent> OrdersSurveySecond { get; set; }
    }
}
