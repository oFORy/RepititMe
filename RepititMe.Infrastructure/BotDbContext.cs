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
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Science>().HasData(
               new Science { Name = "Математика" },
               new Science { Name = "Английский язык" },
               new Science { Name = "История" },
               new Science { Name = "Информатика" },
               new Science { Name = "Русский язык" },
               new Science { Name = "Физика" },
               new Science { Name = "Химия" },
               new Science { Name = "Биология" },
               new Science { Name = "Обществознание" }
               );

            modelBuilder.Entity<AgeСategory>().HasData(
                new AgeСategory { Name = "Дошкольники: 4-7 лет" },
                new AgeСategory { Name = "Начальные классы: 1-4 класс" },
                new AgeСategory { Name = "Средние классы: 5-8 класс" },
                new AgeСategory { Name = "Старшие классы: 9-11 класс" },
                new AgeСategory { Name = "Студенты" },
                new AgeСategory { Name = "Взрослые" }
                );

            modelBuilder.Entity<LessonTarget>().HasData(
                new LessonTarget { Name = "ОГЭ по математике" },
                new LessonTarget { Name = "ЕГЭ по математике (базовый уровень)" },
                new LessonTarget { Name = "ЕГЭ по математике (профильный уровень)" },
                new LessonTarget { Name = "Подготовка к олимпиаде" },
                new LessonTarget { Name = "ДВИ по математике" },
                new LessonTarget { Name = "ВПР по математике" },
                new LessonTarget { Name = "Подготовка к экзамену в вузе" },
                new LessonTarget { Name = "Повышение успеваемости" },
                new LessonTarget { Name = "Для себя" }
                );

            modelBuilder.Entity<TeacherStatus>().HasData(
                new TeacherStatus { Name = "Студент" },
                new TeacherStatus { Name = "Аспирант" },
                new TeacherStatus { Name = "Частный преподаватель" },
                new TeacherStatus { Name = "Школьный преподаватель" },
                new TeacherStatus { Name = "Профессор" }
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
