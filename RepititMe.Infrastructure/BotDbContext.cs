using Microsoft.EntityFrameworkCore;
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
                .HasOne(t => t.AgeCategory)
                .WithMany()
                .HasForeignKey(t => t.AgeCategoryId);



            modelBuilder.Entity<Review>()
                .HasOne(t => t.Student)
                .WithMany()
                .HasForeignKey(t => t.StudentId);

            modelBuilder.Entity<Review>()
                .HasOne(t => t.Teacher)
                .WithMany()
                .HasForeignKey(t => t.TeacherId);



            modelBuilder.Entity<Dispute>()
                .HasOne(t => t.Student)
                .WithMany()
                .HasForeignKey(t => t.StudentId);

            modelBuilder.Entity<Dispute>()
                .HasOne(t => t.Teacher)
                .WithMany()
                .HasForeignKey(t => t.TeacherId);



            modelBuilder.Entity<Order>()
                .HasMany(o => o.Reports)
                .WithOne(r => r.Order)
                .HasForeignKey(r => r.OrderId);

            modelBuilder.Entity<Order>()
                .HasOne(t => t.Teacher)
                .WithMany()
                .HasForeignKey(t => t.TeacherId);

            modelBuilder.Entity<Order>()
                .HasOne(t => t.Student)
                .WithMany()
                .HasForeignKey(t => t.StudentId);



            modelBuilder.Entity<SurveyFirst>()
                .HasOne(t => t.Order)
                .WithMany()
                .HasForeignKey(t => t.OrderId);



            modelBuilder.Entity<ScienceLessonTarget>()
                .HasKey(slt => new { slt.ScienceId, slt.LessonTargetId });

            modelBuilder.Entity<ScienceLessonTarget>()
                .HasOne(slt => slt.Science)
                .WithMany(s => s.ScienceLessonTargets)
                .HasForeignKey(slt => slt.ScienceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ScienceLessonTarget>()
                .HasOne(slt => slt.LessonTarget)
                .WithMany(lt => lt.ScienceLessonTargets)
                .HasForeignKey(slt => slt.LessonTargetId)
                .OnDelete(DeleteBehavior.Cascade);



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

            modelBuilder.Entity<AgeCategory>().HasData(
                new AgeCategory { Id = 1, Name = "Дошкольники: 4-7 лет" },
                new AgeCategory { Id = 2, Name = "Начальные классы: 1-4 класс" },
                new AgeCategory { Id = 3, Name = "Средние классы: 5-8 класс" },
                new AgeCategory { Id = 4, Name = "Старшие классы: 9-11 класс" },
                new AgeCategory { Id = 5, Name = "Студенты" },
                new AgeCategory { Id = 6, Name = "Взрослые" }
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


            modelBuilder.Entity<ScienceLessonTarget>().HasData(
                new ScienceLessonTarget { ScienceId = 1, LessonTargetId = 1 },
                new ScienceLessonTarget { ScienceId = 1, LessonTargetId = 2 },
                new ScienceLessonTarget { ScienceId = 1, LessonTargetId = 3 },
                new ScienceLessonTarget { ScienceId = 1, LessonTargetId = 4 },
                new ScienceLessonTarget { ScienceId = 1, LessonTargetId = 5 },
                new ScienceLessonTarget { ScienceId = 1, LessonTargetId = 6 },
                new ScienceLessonTarget { ScienceId = 1, LessonTargetId = 7 },
                new ScienceLessonTarget { ScienceId = 1, LessonTargetId = 8 },
                new ScienceLessonTarget { ScienceId = 1, LessonTargetId = 9 },
                new ScienceLessonTarget { ScienceId = 2, LessonTargetId = 8 },
                new ScienceLessonTarget { ScienceId = 2, LessonTargetId = 9 },
                new ScienceLessonTarget { ScienceId = 3, LessonTargetId = 4 },
                new ScienceLessonTarget { ScienceId = 3, LessonTargetId = 9 },
                new ScienceLessonTarget { ScienceId = 4, LessonTargetId = 9 },
                new ScienceLessonTarget { ScienceId = 5, LessonTargetId = 9 },
                new ScienceLessonTarget { ScienceId = 6, LessonTargetId = 9 },
                new ScienceLessonTarget { ScienceId = 7, LessonTargetId = 9 },
                new ScienceLessonTarget { ScienceId = 8, LessonTargetId = 8 },
                new ScienceLessonTarget { ScienceId = 8, LessonTargetId = 9 },
                new ScienceLessonTarget { ScienceId = 9, LessonTargetId = 8 },
                new ScienceLessonTarget { ScienceId = 9, LessonTargetId = 9 }
            );

            modelBuilder.Entity<StudentUseFulUrl>().HasData(
                new StudentUseFulUrl { Id = 1, Image = "https://www.umi-cms.ru/images/cms/data/articles/webp/webp3.jpg", Url = "http://link1.com", Text = "Текст ссылки", ColorText = "#FFFFFF" },
                new StudentUseFulUrl { Id = 2, Image = "https://www.umi-cms.ru/images/cms/data/articles/webp/webp3.jpg", Url = "http://link2.com", Text = "Текст ссылки", ColorText = "#000000" },
                new StudentUseFulUrl { Id = 3, Image = "https://www.umi-cms.ru/images/cms/data/articles/webp/webp3.jpg", Url = "http://link3.com", Text = "Текст ссылки", ColorText = "#FFFFFF" },
                new StudentUseFulUrl { Id = 4, Image = "https://www.umi-cms.ru/images/cms/data/articles/webp/webp3.jpg", Url = "http://link4.com", Text = "Текст ссылки", ColorText = "#FFFFFF" },
                new StudentUseFulUrl { Id = 5, Image = "https://www.umi-cms.ru/images/cms/data/articles/webp/webp3.jpg", Url = "http://link5.com", Text = "Текст ссылки", ColorText = "#000000" }
            );

            modelBuilder.Entity<TeacherUseFulUrl>().HasData(
                new TeacherUseFulUrl { Id = 1, Image = "https://www.umi-cms.ru/images/cms/data/articles/webp/webp3.jpg", Url = "http://link1.com", Text = "Текст ссылки", ColorText = "#000000" },
                new TeacherUseFulUrl { Id = 2, Image = "https://www.umi-cms.ru/images/cms/data/articles/webp/webp3.jpg", Url = "http://link2.com", Text = "Текст ссылки", ColorText = "#FFFFFF" },
                new TeacherUseFulUrl { Id = 3, Image = "https://www.umi-cms.ru/images/cms/data/articles/webp/webp3.jpg", Url = "http://link3.com", Text = "Текст ссылки", ColorText = "#000000" }
            );

            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { Id = 1, UserId = 2, PaymentRating = 700, Rating = 5.0, Image = "testteacher", StatusId = 5, ScienceId = 2, LessonTargetId = 1, AgeCategoryId  = 3, Experience = 2, Price = 2500, Visibility = true},
                new Teacher { Id = 2, UserId = 3, PaymentRating = 1000, Rating = 4.4, Image = "testteacher2", StatusId = 2, ScienceId = 1, LessonTargetId = 3, AgeCategoryId = 4, Experience = 1, Price = 1200, Visibility = false}
                );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Сергей", TelegramName = "@Serg" ,TelegramId = 12, LastActivity = 1, Block = false },
                new User { Id = 2, Name = "Владимир", TelegramName = "@Vlad", SecondName = "Петров", TelegramId = 23, LastActivity = 2, Block = false },
                new User { Id = 3, Name = "Даниил", TelegramName = "@Dania", SecondName = "Семенов", TelegramId = 34, LastActivity = 1 , Block = false }
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
        public DbSet<AgeCategory> AgeCategories { get; set; }
        public DbSet<TeacherStatus> TeacherStatuses { get; set; }
        public DbSet<ScienceLessonTarget> ScienceLessonTargets { get; set; }
        public DbSet<SurveyFirst> SurveisFirst { get; set; }
        public DbSet<SurveySecond> SurveisSecond { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Dispute> Disputes { get; set; }
    }
}
