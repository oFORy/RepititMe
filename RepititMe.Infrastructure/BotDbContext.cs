﻿using Microsoft.EntityFrameworkCore;
using RepititMe.Domain.Entities;
using RepititMe.Domain.Entities.Data;
using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Entities.Weights;

namespace RepititMe.Infrastructure
{
    public class BotDbContext : DbContext
    {
        public BotDbContext(DbContextOptions<BotDbContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Status)
                .WithMany()
                .HasForeignKey(t => t.StatusId);

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Science)
                .WithMany()
                .HasForeignKey(t => t.ScienceId);

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.LessonTarget)
                .WithMany()
                .HasForeignKey(t => t.LessonTargetId);

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.AgeСategory)
                .WithMany()
                .HasForeignKey(t => t.AgeСategoryId);







            modelBuilder.Entity<Science>().HasData(
               new Science { Id = 1, Name = "Математика" },
               new Science { Id = 2, Name = "Английский язык" },
               new Science { Id = 3, Name = "История" },
               new Science { Id = 4, Name = "Информатика" },
               new Science { Id = 5, Name = "Русский язык" },
               new Science { Id = 6, Name = "Физика" },
               new Science { Id = 7, Name = "Химия" },
               new Science { Id = 8, Name = "Биология" },
               new Science { Id = 9, Name = "Обществознание" }
               );

            modelBuilder.Entity<AgeСategory>().HasData(
                new AgeСategory { Id = 1, Name = "Дошкольники: 4-7 лет" },
                new AgeСategory { Id = 2, Name = "Начальные классы: 1-4 класс" },
                new AgeСategory { Id = 3, Name = "Средние классы: 5-8 класс" },
                new AgeСategory { Id = 4, Name = "Старшие классы: 9-11 класс" },
                new AgeСategory { Id = 5, Name = "Студенты" },
                new AgeСategory { Id = 6, Name = "Взрослые" }
                );

            modelBuilder.Entity<LessonTarget>().HasData(
                new LessonTarget { Id = 1, Name = "ОГЭ по математике" },
                new LessonTarget { Id = 2, Name = "ЕГЭ по математике (базовый уровень)" },
                new LessonTarget { Id = 3, Name = "ЕГЭ по математике (профильный уровень)" },
                new LessonTarget { Id = 4, Name = "Подготовка к олимпиаде" },
                new LessonTarget { Id = 5, Name = "ДВИ по математике" },
                new LessonTarget { Id = 6, Name = "ВПР по математике" },
                new LessonTarget { Id = 7, Name = "Подготовка к экзамену в вузе" },
                new LessonTarget { Id = 8, Name = "Повышение успеваемости" },
                new LessonTarget { Id = 9, Name = "Для себя" }
                );

            modelBuilder.Entity<TeacherStatus>().HasData(
                new TeacherStatus { Id = 1, Name = "Студент" },
                new TeacherStatus { Id = 2, Name = "Аспирант" },
                new TeacherStatus { Id = 3, Name = "Частный преподаватель" },
                new TeacherStatus { Id = 4, Name = "Школьный преподаватель" },
                new TeacherStatus { Id = 5, Name = "Профессор" }
                );


            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, UserId = 1},
                new Student { Id = 2, UserId = 3}
                );

            modelBuilder.Entity<StudentUseFulUrl>().HasData(
                new StudentUseFulUrl { Id = 1, Image = "https://www.umi-cms.ru/images/cms/data/articles/webp/webp3.jpg", Url = "http://link1.com" },
                new StudentUseFulUrl { Id = 2, Image = "https://www.umi-cms.ru/images/cms/data/articles/webp/webp3.jpg", Url = "http://link2.com" },
                new StudentUseFulUrl { Id = 3, Image = "https://www.umi-cms.ru/images/cms/data/articles/webp/webp3.jpg", Url = "http://link3.com" },
                new StudentUseFulUrl { Id = 4, Image = "https://www.umi-cms.ru/images/cms/data/articles/webp/webp3.jpg", Url = "http://link4.com" },
                new StudentUseFulUrl { Id = 5, Image = "https://www.umi-cms.ru/images/cms/data/articles/webp/webp3.jpg", Url = "http://link5.com" }
            );

            modelBuilder.Entity<TeacherUseFulUrl>().HasData(
                new TeacherUseFulUrl { Id = 1, Image = "https://www.umi-cms.ru/images/cms/data/articles/webp/webp3.jpg", Url = "http://link1.com" },
                new TeacherUseFulUrl { Id = 2, Image = "https://www.umi-cms.ru/images/cms/data/articles/webp/webp3.jpg", Url = "http://link2.com" },
                new TeacherUseFulUrl { Id = 3, Image = "https://www.umi-cms.ru/images/cms/data/articles/webp/webp3.jpg", Url = "http://link3.com" }
            );

            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { Id = 1, UserId = 2, PaymentRating = 700, Rating = 5.0, Image = "testteacher", StatusId = 5, ScienceId = 2, LessonTargetId = 1, AgeСategoryId  = 3, Experience = "2 Года", Price = 2500, Visibility = true, Block = false },
                new Teacher { Id = 2, UserId = 3, PaymentRating = 1000, Rating = 4.4, Image = "testteacher2", StatusId = 2, ScienceId = 1, LessonTargetId = 3, AgeСategoryId = 4, Experience = "3 Года", Price = 1200, Visibility = false, Block = false }
                );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Сергей", TelegramId = 12, LastActivity = 1},
                new User { Id = 2, Name = "Владимир", SecondName = "Петров", TelegramId = 23, LastActivity = 2 },
                new User { Id = 3, Name = "Даниил", SecondName = "Семенов", TelegramId = 34, LastActivity = 1 }
                );
        }


        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<StudentUseFulUrl> StudentUseFulUrls { get; set; }
        public DbSet<TeacherUseFulUrl> TeacherUseFulUrls { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Science> Sciences { get; set; }
        public DbSet<LessonTarget> LessonTargets { get; set; }
        public DbSet<AgeСategory> AgeСategories { get; set; }
        public DbSet<TeacherStatus> TeacherStatuses { get; set; }
    }
}
