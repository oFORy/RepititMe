﻿using Microsoft.EntityFrameworkCore;
using RepititMe.Application.Services.Orders.Common;
using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Entities.Weights;
using RepititMe.Domain.Object.Orders;
using RepititMe.Domain.Object.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace RepititMe.Infrastructure.Persistence
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BotDbContext _botDbContext;
        public OrderRepository(BotDbContext context)
        {
            _botDbContext = context;
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
                var newSurvey = new Survey()
                {
                    TelegramIdStudent = order.Student.User.TelegramId,
                    TelegramIdTeacher = order.Teacher.User.TelegramId,
                    OrderId = idOrder,
                    StudentAnswer = false,
                    TeacherAnswer = false
                };

                await _botDbContext.AddAsync(newSurvey);
                await _botDbContext.SaveChangesAsync();


                order.DateTimeAccept = DateTime.UtcNow;
                return await _botDbContext.SaveChangesAsync() > 0;
            }
            else
                return false;
        }

        public async Task<bool> CancelOrder(int idOrder)
        {
            var order = await _botDbContext.Orders.FirstOrDefaultAsync(o => o.Id == idOrder);
            if (order != null)
            {
                _botDbContext.Orders.Remove(order);
                return await _botDbContext.SaveChangesAsync() > 0;
            }
            else
                return false;
        }

        public async Task<bool> RefuseOrder(int idOrder)
        {
            var order = await _botDbContext.Orders.FirstOrDefaultAsync(o => o.Id == idOrder);
            if (order != null)
            {
                order.IsRefused = true;
                return await _botDbContext.SaveChangesAsync() > 0;
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
                IsRefused = false,
                IsPaid = false
            };

            _botDbContext.Orders.Add(newOrder);

            return await _botDbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Order>> ShowAllOrdersStudent(int telegramId)
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
                .Where(o => o.StudentId == studentId && !o.IsRefused && !o.IsPaid)
                .Include(o => o.Teacher)
                    .ThenInclude(t => t.User)
                .Include(o => o.Teacher)
                    .ThenInclude(t => t.Status)
                .Include(o => o.Teacher)
                    .ThenInclude(t => t.Science)
                .Include(o => o.Teacher)
                    .ThenInclude(t => t.LessonTarget)
                .Include(o => o.Teacher)
                    .ThenInclude(t => t.AgeCategory)
                .ToListAsync();

            return studentOrders;
        }

        public async Task<List<Order>> ShowAllOrdersTeacher(int telegramId)
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
                .Where(o => o.TeacherId == teacherId && !o.IsRefused && !o.IsPaid)
                .Include(o => o.Student)
                    .ThenInclude(t => t.User)
                .ToListAsync();

            return teacherOrders;
        }
    }
}
