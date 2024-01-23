using Microsoft.EntityFrameworkCore;
using RepititMe.Application.bot.Services;
using RepititMe.Application.Services.Orders.Common;
using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Entities.Weights;
using RepititMe.Domain.Object.Orders;
using RepititMe.Domain.Object.Reviews;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace RepititMe.Infrastructure.Persistence
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BotDbContext _botDbContext;
        private readonly ITelegramService _telegramService;
        public OrderRepository(BotDbContext context, ITelegramService telegramService)
        {
            _botDbContext = context;
            _telegramService = telegramService;
        }

        public async Task<bool> AcceptOrder(int idOrder)
        {
            var order = await _botDbContext.Orders
                    .Include(o => o.Student)
                        .ThenInclude(s => s.User)
                    .Include(o => o.Teacher)
                        .ThenInclude(t => t.User)
                    .FirstOrDefaultAsync(o => o.Id == idOrder);

            if (order != null)
            {
                var newSurvey = new SurveyFirst()
                {
                    TelegramIdStudent = order.Student.User.TelegramId,
                    TelegramIdTeacher = order.Teacher.User.TelegramId,
                    OrderId = idOrder,
                    StudentAnswer = false,
                    TeacherAnswer = false
                };

                await _botDbContext.AddAsync(newSurvey);
                if (await _botDbContext.SaveChangesAsync() == 0)
                    return false;


                order.DateTimeAccept = DateTime.UtcNow;
                if (await _botDbContext.SaveChangesAsync() == 0)
                    return false;



                string messageTeacher = $"Данные для связи с учеником: {order.Student.User.Name} {order.Student.User.TelegramName}";
                await _telegramService.SendActionAsync(messageTeacher, order.Teacher.User.TelegramId.ToString());

                string messageStudent = $"Данные для связи с преподавателем: {order.Teacher.User.Name} {order.Teacher.User.TelegramName}";
                await _telegramService.SendActionAsync(messageStudent, order.Student.User.TelegramId.ToString());


                string message = $"Учитель ({order.Teacher.User.Name} {order.Teacher.User.SecondName} | {order.Teacher.User.TelegramName}) и ученик ({order.Student.User.Name} | {order.Student.User.TelegramName}) обменялись контактами";
                List<long> admins = await _botDbContext.Users.Where(u => u.Admin).Select(u => u.TelegramId).ToListAsync();
                foreach (long adminId in admins)
                {
                    await _telegramService.SendActionAsync(message, adminId.ToString());
                }

                return true;
            }
            else
                return false;
        }

        public async Task<bool> CancelOrder(int idOrder)
        {
            var order = await _botDbContext.Orders
                .Include(o => o.Student).ThenInclude(s => s.User)
                .FirstOrDefaultAsync(o => o.Id == idOrder);

            if (order != null)
            {
                string messageStudent = $"К сожалению репетитор {order.Teacher.User.Name} {order.Teacher.User.TelegramName} сейчас не может взять вас на занятия, попробуйте подобрать другого преподавателя.";
                await _telegramService.SendActionAsync(messageStudent, order.Student.User.TelegramId.ToString());

                _botDbContext.Orders.Remove(order);
                return await _botDbContext.SaveChangesAsync() > 0;
            }
            else
                return false;
        }

        public async Task<bool> RefuseOrder(RefuseOrederObject refuseOrederObject)
        {
            var order = await _botDbContext.Orders.FirstOrDefaultAsync(o => o.Id == refuseOrederObject.IdOrder);
            if (order != null)
            {
                if (refuseOrederObject.User == 2)
                {
                    order.RefusedTeacher = true;
                    if (refuseOrederObject.DescriptionRefuse != null)
                    {
                        string message = $"Учитель ({order.Teacher.User.Name} {order.Teacher.User.SecondName} | {order.Teacher.User.TelegramName}) отказался от ученика ({order.Student.User.Name} | {order.Student.User.TelegramName}) | Описание: {refuseOrederObject.DescriptionRefuse}";
                        List<long> admins = await _botDbContext.Users.Where(u => u.Admin).Select(u => u.TelegramId).ToListAsync();
                        foreach (long adminId in admins)
                        {
                            await _telegramService.SendActionAsync(message, adminId.ToString());
                        }
                    }
                    return await _botDbContext.SaveChangesAsync() > 0;
                } 
                else
                {
                    order.RefusedStudent = true;
                    return await _botDbContext.SaveChangesAsync() > 0;
                }
                    
            }
            else
                return false;
        }

        public async Task<bool> NewOrder(NewOrderObject newOrderObject)
        {
            var userId = await _botDbContext.Users
                .Where(u => u.TelegramId == newOrderObject.TelegramIdTeacher)
                .Select(u => u.Id)
                .SingleOrDefaultAsync();

            var teacherId = await _botDbContext.Teachers
                .Where(s => s.UserId == userId)
                .Select(s => s.Id)
                .SingleOrDefaultAsync();

            var userId2 = await _botDbContext.Users
                .Where(u => u.TelegramId == newOrderObject.TelegramIdStudent)
                .Select(u => u.Id)
                .SingleOrDefaultAsync();

            var studentId = await _botDbContext.Students
                .Where(s => s.UserId == userId2)
                .Select(s => s.Id)
                .SingleOrDefaultAsync();

            var newOrder = new Order()
            {
                TeacherId = teacherId,
                StudentId = studentId,
                Description = newOrderObject.Description,
                RefusedStudent = false,
                RefusedTeacher = false,
                Commission = 0
            };

            _botDbContext.Orders.Add(newOrder);


            var stud = await _botDbContext.Students.Include(u => u.User).SingleOrDefaultAsync(u => u.User.TelegramId == newOrderObject.TelegramIdStudent);
            string message = $"У вас новая заявка от ученика | {stud?.User.Name}";
            await _telegramService.SendActionAsync(message, newOrderObject.TelegramIdTeacher.ToString());

            return await _botDbContext.SaveChangesAsync() > 0;
        }

        public async Task<ShowAllOrdersObject> ShowAllOrdersStudent(long telegramId)
        {
            var userId = await _botDbContext.Users
                .Where(u => u.TelegramId == telegramId)
                .Select(u => u.Id)
                .SingleOrDefaultAsync();

            var studentId = await _botDbContext.Students
                .Where(s => s.UserId == userId)
                .Select(s => s.Id)
                .SingleOrDefaultAsync();

            var studentOrders = await _botDbContext.Orders
                .Where(o => o.StudentId == studentId && !o.RefusedStudent && !o.RefusedTeacher)
                .Include(o => o.Teacher)
                    .ThenInclude(t => t.User)
                .Include(o => o.Teacher)
                    .ThenInclude(t => t.Status)
                .Include(o => o.Teacher)
                    .ThenInclude(t => t.Science)
                .Include(o => o.Teacher)
                    .ThenInclude(t => t.TeacherLessonTargets)
                        .ThenInclude(tlt => tlt.LessonTarget)
                .Include(o => o.Teacher)
                    .ThenInclude(t => t.TeacherAgeCategories)
                        .ThenInclude(tac => tac.AgeCategory)
                .ToListAsync();


            List<int> countLessons = new List<int>();

            foreach (var order in studentOrders)
            {
                var count = await _botDbContext.Reports
                    .CountAsync(r => r.OrderId == order.Id);
                countLessons.Add(count);
            }


            return new ShowAllOrdersObject { Orders = studentOrders , CountLesson = countLessons };
        }

        public async Task<ShowAllOrdersObject> ShowAllOrdersTeacher(long telegramId)
        {
            var userId = await _botDbContext.Users
                .Where(u => u.TelegramId == telegramId)
                .Select(u => u.Id)
                .SingleOrDefaultAsync();

            var teacherId = await _botDbContext.Teachers
                .Where(s => s.UserId == userId)
                .Select(s => s.Id)
                .SingleOrDefaultAsync();

            var teacherOrders = await _botDbContext.Orders
                .Where(o => o.Commission > 0 ? (o.TeacherId == teacherId && o.Commission > 0) : (o.TeacherId == teacherId && !o.RefusedTeacher))
                .Include(o => o.Student)
                    .ThenInclude(t => t.User)
                .ToListAsync();

            List<int> countLessons = new List<int>();

            foreach (var order in teacherOrders)
            {
                var count = await _botDbContext.Reports
                    .CountAsync(r => r.OrderId == order.Id);
                countLessons.Add(count);
            }


            return new ShowAllOrdersObject { Orders = teacherOrders, CountLesson = countLessons };
        }
    }
}
