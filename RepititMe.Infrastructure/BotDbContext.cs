using Microsoft.EntityFrameworkCore;
using RepititMe.Domain.Entities;

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
                new Student { Id = 1, Image = "test", Name = "Сергей", TelegramId = 12, UsefulLinks = new List<string> { "http://link1.com", "http://link2.com", "http://link3.com" } },
                new Student { Id = 2, Image = "test2", Name = "Даниил", TelegramId = 34, UsefulLinks = new List<string> { "http://link1.com", "http://link2.com", "http://link3.com" } }
                );

            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { Id = 1, Image = "testteacher", Name = "Владимир", SecondName = "Петров", TelegramId = 23, StatusId = 5, ScienceId = 2, LessonTargetId = 1, AgeСategoryId  = 3, Experience = "2 Года", Price = 2500, UsefulLinks = new List<string> { "http://link1.com", "http://link2.com", "http://link3.com" }, Visibility = true},
                new Teacher { Id = 2, Image = "testteacher2", Name = "Даниил", SecondName = "Семенов", TelegramId = 34, StatusId = 2, ScienceId = 1, LessonTargetId = 3, AgeСategoryId = 4, Experience = "2 Года", Price = 1200, UsefulLinks = new List<string> { "http://link1.com", "http://link2.com", "http://link3.com" }, Visibility = false }
                );
        }


        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Science> Sciences { get; set; }
        public DbSet<LessonTarget> LessonTargets { get; set; }
        public DbSet<AgeСategory> AgeСategories { get; set; }
        public DbSet<TeacherStatus> TeacherStatuses { get; set; }
    }
}
