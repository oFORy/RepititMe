using RepititMe.Application.Services.Student.Common;
using RepititMe.Domain.Entities;
using RepititMe.Domain.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Infrastructure.Persistence
{
    public class StudentRepository : IStudentRepository
    {
        private readonly BotDbContext _botDbContext;
        public StudentRepository(BotDbContext context)
        {
            _botDbContext = context;
        }

        public Task<bool> ChangeProfile(int telegramId, string image = null, string name = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<Teacher>> ShowTeachers(int lastNumber)
        {
            throw new NotImplementedException();
        }

        public Task<SignInStudentObject> SignInStudent(int telegramId)
        {
            throw new NotImplementedException();
        }
    }
}
