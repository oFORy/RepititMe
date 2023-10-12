﻿using RepititMe.Application.Services.Student.Common;
using RepititMe.Domain.Entities;
using RepititMe.Domain.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Student.Queries
{
    public class StudentQueryService : IStudentQueryService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentQueryService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<List<Teacher>> ShowTeachers(int lastNumber)
        {
            return await _studentRepository.ShowTeachers(lastNumber);
        }

        public async Task<SignInStudentObject> SignInStudent(int telegramId)
        {
            return await _studentRepository.SignInStudent(telegramId);
        }
    }
}