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
            //Database.SetCommandTimeout(60);
            //Database.EnsureDeleted();
            Database.EnsureCreated();
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

            modelBuilder.Entity<TeacherLessonTarget>()
                .HasKey(tlt => new { tlt.TeacherId, tlt.LessonTargetId });

            modelBuilder.Entity<TeacherAgeCategory>()
                .HasKey(tac => new { tac.TeacherId, tac.AgeCategoryId });

            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.TeacherLessonTargets)
                .WithOne(tlt => tlt.Teacher)
                .HasForeignKey(tlt => tlt.TeacherId);

            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.TeacherAgeCategories)
                .WithOne(tac => tac.Teacher)
                .HasForeignKey(tac => tac.TeacherId);



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



            modelBuilder.Entity<Report>()
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
                new LessonTarget { Id = 4, Name = "Подготовка к олимпиаде по математике" },
                new LessonTarget { Id = 5, Name = "ДВИ по математике" },
                new LessonTarget { Id = 6, Name = "ВПР по математике" },
                new LessonTarget { Id = 7, Name = "Подготовка к экзамену в вузе" },
                new LessonTarget { Id = 8, Name = "Повышение успеваемости" },
                new LessonTarget { Id = 9, Name = "Для себя" },

                new LessonTarget { Id = 10, Name = "Подготовка к олимпиаде по английскому языку" },
                new LessonTarget { Id = 11, Name = "ОГЭ по английскому языку" },
                new LessonTarget { Id = 12, Name = "ЕГЭ по английскому языку" },
                new LessonTarget { Id = 13, Name = "ДВИ по английскому языку" },
                new LessonTarget { Id = 14, Name = "ВПР по английскому языку" },

                new LessonTarget { Id = 15, Name = "Подготовка к олимпиаде по истории" },
                new LessonTarget { Id = 16, Name = "ОГЭ по истории" },
                new LessonTarget { Id = 17, Name = "ЕГЭ по истории" },
                new LessonTarget { Id = 18, Name = "ДВИ по истории" },
                new LessonTarget { Id = 19, Name = "ВПР по истории" },

                new LessonTarget { Id = 20, Name = "Подготовка к олимпиаде по информатике" },
                new LessonTarget { Id = 21, Name = "ОГЭ по информатике" },
                new LessonTarget { Id = 22, Name = "ЕГЭ по информатике" },
                new LessonTarget { Id = 23, Name = "ДВИ по информатике" },
                new LessonTarget { Id = 24, Name = "ВПР по информатике" },

                new LessonTarget { Id = 25, Name = "Подготовка к олимпиаде по русскому языку" },
                new LessonTarget { Id = 26, Name = "ОГЭ по русскому языку" },
                new LessonTarget { Id = 27, Name = "ЕГЭ по русскому языку" },
                new LessonTarget { Id = 28, Name = "ДВИ по русскому языку" },
                new LessonTarget { Id = 29, Name = "ВПР по русскому языку" },

                new LessonTarget { Id = 30, Name = "Подготовка к олимпиаде по физике" },
                new LessonTarget { Id = 31, Name = "ОГЭ по физике" },
                new LessonTarget { Id = 32, Name = "ЕГЭ по физике" },
                new LessonTarget { Id = 33, Name = "ДВИ по физике" },
                new LessonTarget { Id = 34, Name = "ВПР по физике" },

                new LessonTarget { Id = 35, Name = "Подготовка к олимпиаде по химии" },
                new LessonTarget { Id = 36, Name = "ОГЭ по химии" },
                new LessonTarget { Id = 37, Name = "ЕГЭ по химии" },
                new LessonTarget { Id = 38, Name = "ДВИ по химии" },
                new LessonTarget { Id = 39, Name = "ВПР по химии" },

                new LessonTarget { Id = 40, Name = "Подготовка к олимпиаде по биологии" },
                new LessonTarget { Id = 41, Name = "ОГЭ по биологии" },
                new LessonTarget { Id = 42, Name = "ЕГЭ по биологии" },
                new LessonTarget { Id = 43, Name = "ДВИ по биологии" },
                new LessonTarget { Id = 44, Name = "ВПР по биологии" },

                new LessonTarget { Id = 45, Name = "Подготовка к олимпиаде по обществознанию" },
                new LessonTarget { Id = 46, Name = "ОГЭ по обществознанию" },
                new LessonTarget { Id = 47, Name = "ЕГЭ по обществознанию" },
                new LessonTarget { Id = 48, Name = "ДВИ по обществознанию" },
                new LessonTarget { Id = 49, Name = "ВПР по обществознанию" }
                );

            modelBuilder.Entity<TeacherStatus>().HasData(
                new TeacherStatus { Id = 1, Name = "Студент" },
                new TeacherStatus { Id = 2, Name = "Аспирант" },
                new TeacherStatus { Id = 3, Name = "Частный преподаватель" },
                new TeacherStatus { Id = 4, Name = "Школьный преподаватель" },
                new TeacherStatus { Id = 5, Name = "Профессор" }
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

                new ScienceLessonTarget { ScienceId = 2, LessonTargetId = 7 },
                new ScienceLessonTarget { ScienceId = 2, LessonTargetId = 8 },
                new ScienceLessonTarget { ScienceId = 2, LessonTargetId = 9 },
                new ScienceLessonTarget { ScienceId = 2, LessonTargetId = 10 },
                new ScienceLessonTarget { ScienceId = 2, LessonTargetId = 11 },
                new ScienceLessonTarget { ScienceId = 2, LessonTargetId = 12 },
                new ScienceLessonTarget { ScienceId = 2, LessonTargetId = 13 },
                new ScienceLessonTarget { ScienceId = 2, LessonTargetId = 14 },

                new ScienceLessonTarget { ScienceId = 3, LessonTargetId = 7 },
                new ScienceLessonTarget { ScienceId = 3, LessonTargetId = 8 },
                new ScienceLessonTarget { ScienceId = 3, LessonTargetId = 9 },
                new ScienceLessonTarget { ScienceId = 3, LessonTargetId = 15 },
                new ScienceLessonTarget { ScienceId = 3, LessonTargetId = 16 },
                new ScienceLessonTarget { ScienceId = 3, LessonTargetId = 17 },
                new ScienceLessonTarget { ScienceId = 3, LessonTargetId = 18 },
                new ScienceLessonTarget { ScienceId = 3, LessonTargetId = 19 },

                new ScienceLessonTarget { ScienceId = 4, LessonTargetId = 7 },
                new ScienceLessonTarget { ScienceId = 4, LessonTargetId = 8 },
                new ScienceLessonTarget { ScienceId = 4, LessonTargetId = 9 },
                new ScienceLessonTarget { ScienceId = 4, LessonTargetId = 20 },
                new ScienceLessonTarget { ScienceId = 4, LessonTargetId = 21 },
                new ScienceLessonTarget { ScienceId = 4, LessonTargetId = 22 },
                new ScienceLessonTarget { ScienceId = 4, LessonTargetId = 23 },
                new ScienceLessonTarget { ScienceId = 4, LessonTargetId = 24 },

                new ScienceLessonTarget { ScienceId = 5, LessonTargetId = 7 },
                new ScienceLessonTarget { ScienceId = 5, LessonTargetId = 8 },
                new ScienceLessonTarget { ScienceId = 5, LessonTargetId = 9 },
                new ScienceLessonTarget { ScienceId = 5, LessonTargetId = 25 },
                new ScienceLessonTarget { ScienceId = 5, LessonTargetId = 26 },
                new ScienceLessonTarget { ScienceId = 5, LessonTargetId = 27 },
                new ScienceLessonTarget { ScienceId = 5, LessonTargetId = 28 },
                new ScienceLessonTarget { ScienceId = 5, LessonTargetId = 29 },

                new ScienceLessonTarget { ScienceId = 6, LessonTargetId = 7 },
                new ScienceLessonTarget { ScienceId = 6, LessonTargetId = 8 },
                new ScienceLessonTarget { ScienceId = 6, LessonTargetId = 9 },
                new ScienceLessonTarget { ScienceId = 6, LessonTargetId = 30 },
                new ScienceLessonTarget { ScienceId = 6, LessonTargetId = 31 },
                new ScienceLessonTarget { ScienceId = 6, LessonTargetId = 32 },
                new ScienceLessonTarget { ScienceId = 6, LessonTargetId = 33 },
                new ScienceLessonTarget { ScienceId = 6, LessonTargetId = 34 },


                new ScienceLessonTarget { ScienceId = 7, LessonTargetId = 7 },
                new ScienceLessonTarget { ScienceId = 7, LessonTargetId = 8 },
                new ScienceLessonTarget { ScienceId = 7, LessonTargetId = 9 },
                new ScienceLessonTarget { ScienceId = 7, LessonTargetId = 35 },
                new ScienceLessonTarget { ScienceId = 7, LessonTargetId = 36 },
                new ScienceLessonTarget { ScienceId = 7, LessonTargetId = 37 },
                new ScienceLessonTarget { ScienceId = 7, LessonTargetId = 38 },
                new ScienceLessonTarget { ScienceId = 7, LessonTargetId = 39 },


                new ScienceLessonTarget { ScienceId = 8, LessonTargetId = 7 },
                new ScienceLessonTarget { ScienceId = 8, LessonTargetId = 8 },
                new ScienceLessonTarget { ScienceId = 8, LessonTargetId = 9 },
                new ScienceLessonTarget { ScienceId = 8, LessonTargetId = 40 },
                new ScienceLessonTarget { ScienceId = 8, LessonTargetId = 41 },
                new ScienceLessonTarget { ScienceId = 8, LessonTargetId = 42 },
                new ScienceLessonTarget { ScienceId = 8, LessonTargetId = 43 },
                new ScienceLessonTarget { ScienceId = 8, LessonTargetId = 44 },


                new ScienceLessonTarget { ScienceId = 9, LessonTargetId = 7 },
                new ScienceLessonTarget { ScienceId = 9, LessonTargetId = 8 },
                new ScienceLessonTarget { ScienceId = 9, LessonTargetId = 9 },
                new ScienceLessonTarget { ScienceId = 9, LessonTargetId = 45 },
                new ScienceLessonTarget { ScienceId = 9, LessonTargetId = 46 },
                new ScienceLessonTarget { ScienceId = 9, LessonTargetId = 47 },
                new ScienceLessonTarget { ScienceId = 9, LessonTargetId = 48 },
                new ScienceLessonTarget { ScienceId = 9, LessonTargetId = 49 }
            );

            modelBuilder.Entity<StudentUseFulUrl>().HasData(
                new StudentUseFulUrl { Id = 1, Image = "https://img.freepik.com/free-photo/representations-user-experience-interface-design_23-2150104504.jpg?w=1380&t=st=1699363487~exp=1699364087~hmac=a9b760c1990278d66e9109276b979b59e76c80ca595013e73eba0940c56d26cc", Url = "https://teletype.in/@repetitme/about_us", Text = "Как пользоваться сервисом? ", ColorText = "#FFFFFF" },
                new StudentUseFulUrl { Id = 2, Image = "https://img.freepik.com/free-photo/golden-correct-sign-best-quality-assurance-guarantee-product-iso-service-concept_616485-97.jpg?w=1380&t=st=1699363513~exp=1699364113~hmac=ecaf72d45234a4bb1219570234da5ceabf78e3bf10a97af84e00afb49fcd5ae5", Url = "https://teletype.in/@repetitme/adv_for_students", Text = "Преимущества сервиса для клиентов", ColorText = "#000000" },
                new StudentUseFulUrl { Id = 3, Image = "https://img.freepik.com/free-photo/beautiful-rendering-dating-app-concept_23-2149316416.jpg?w=1060&t=st=1699363774~exp=1699364374~hmac=e63b5200fc721a3e448eba417175666be7775c2c0f33da70ee967308a2d0240d", Url = "https://teletype.in/@repetitme/ways_to_find_tutors", Text = "Способы подобрать репетитора", ColorText = "#FFFFFF" },
                new StudentUseFulUrl { Id = 4, Image = "https://img.freepik.com/free-photo/comment-message-inbox-shape-social-media-notification-icon-speech-bubbles-3d-cartoon-banner-website-ui-pink-background-3d-rendering-illustration_56104-1328.jpg?w=996&t=st=1699363791~exp=1699364391~hmac=70384b0d70615256dca861d55637a99b8eeac5a381ef02c0e7768b8de466552e", Url = "https://teletype.in/@repetitme/feedback_students", Text = "Обратная связь от ученика", ColorText = "#FFFFFF" },
                new StudentUseFulUrl { Id = 5, Image = "https://img.freepik.com/free-photo/3d-render-customer-leave-feedback-phone-screen_107791-17460.jpg?w=826&t=st=1699363813~exp=1699364413~hmac=676797ff3134036292b7b0678a890743998fe3ede313c132a1e5fcbd935c6b06", Url = "https://teletype.in/@repetitme/online_lessons", Text = "Как проводить онлайн-занятия?", ColorText = "#000000" },
                new StudentUseFulUrl { Id = 6, Image = "https://img.freepik.com/free-photo/3d-render-online-education-survey-test-concept_107791-15665.jpg?w=1800&t=st=1699363835~exp=1699364435~hmac=1dcdc60cb6f2e8d2e2362d1ff8101f78c0e7f41f79308f22fb2fc3d7185c7986", Url = "https://teletype.in/@repetitme/review", Text = "Как оставить отзыв о репетиторе", ColorText = "#000000" },
                new StudentUseFulUrl { Id = 7, Image = "https://img.freepik.com/free-psd/question-mark-confirmed-signs-false-rejection-icon-3d-render-isolated_47987-11659.jpg?w=1060&t=st=1699363851~exp=1699364451~hmac=f1f533e4f73684627235426e2fbc4992512f9f71b2be48ef10a6adc8a5c1bdcc", Url = "https://teletype.in/@repetitme/faq_students", Text = "FAQ– вопросы учеников", ColorText = "#000000" }
            );

            modelBuilder.Entity<TeacherUseFulUrl>().HasData(
                new TeacherUseFulUrl { Id = 1, Image = "https://img.freepik.com/free-photo/representations-user-experience-interface-design_23-2150104504.jpg?w=1380&t=st=1699363877~exp=1699364477~hmac=1af63fd6e634f7117b35f5828ab2438586596ff114107c04372bb7933069f072", Url = "https://teletype.in/@repetitme/about_us", Text = "Как пользоваться сервисом?", ColorText = "#000000" },
                new TeacherUseFulUrl { Id = 2, Image = "https://img.freepik.com/free-photo/3d-render-bullseye-target-sandglass-coins_107791-16200.jpg?w=1060&t=st=1699363889~exp=1699364489~hmac=f3fb45411408c5c52ae24de4ac5c71cbaa9b5c0300a7d274eb52ad3c557505df", Url = "https://teletype.in/@repetitme/adv_for_tutors", Text = "Преимущества сервиса для репетиторов", ColorText = "#FFFFFF" },
                new TeacherUseFulUrl { Id = 3, Image = "https://img.freepik.com/free-photo/mobile-phone-with-check-list-screen_107791-17459.jpg?w=826&t=st=1699363905~exp=1699364505~hmac=b63d3159070c03c0782365fce92d943e06467d15a5540fe32f3a00c9b03c085f", Url = "https://teletype.in/@repetitme/from_for_tutors", Text = "Анкета репетитора", ColorText = "#FFFFFF" },
                new TeacherUseFulUrl { Id = 4, Image = "https://img.freepik.com/free-photo/golden-russian-ruble-coins-currency-money-sign-symbol-background-3d-illustration_56104-1707.jpg?w=740&t=st=1699363923~exp=1699364523~hmac=71e34623397ca15b7bc078617e449936286fda33eba6809317e99b58159875ed", Url = "https://teletype.in/@repetitme/about_fee", Text = "Как формируется комиссия за заявки?", ColorText = "#FFFFFF" },
                new TeacherUseFulUrl { Id = 5, Image = "https://img.freepik.com/free-photo/five-golden-stars-client-excellent-evaluation-after-use-product-service-concept-by-3d-render_616485-15.jpg?w=1380&t=st=1699363939~exp=1699364539~hmac=c3a33800b559a6ad51484a74095b2863a0c0b459dfc9a3384c2ab4410016c8c6", Url = "https://teletype.in/@repetitme/rating_of_tutor", Text = "Как формируется рейтинг?", ColorText = "#FFFFFF" },
                new TeacherUseFulUrl { Id = 6, Image = "https://img.freepik.com/free-photo/3d-rendering-social-media-icon_23-2150701008.jpg?w=1060&t=st=1699363955~exp=1699364555~hmac=3c1c1242d415454fc889dd2154994aa05bbfdb971d8b1de943be29e7967d3f9c", Url = "https://teletype.in/@repetitme/feedback_tutors", Text = "Уведомления для репетитора", ColorText = "#FFFFFF" },
                new TeacherUseFulUrl { Id = 7, Image = "https://img.freepik.com/free-photo/3d-render-customer-leave-feedback-phone-screen_107791-17460.jpg?w=826&t=st=1699363994~exp=1699364594~hmac=835f40a2ce752e0d61c0d54582e463a8705394efae80722e2310a6a6b45b4746", Url = "https://teletype.in/@repetitme/review", Text = "Как оставить отзыв о репетиторе", ColorText = "#FFFFFF" },
                new TeacherUseFulUrl { Id = 8, Image = "https://img.freepik.com/free-photo/3d-render-online-education-survey-test-concept_107791-15665.jpg?w=1800&t=st=1699364012~exp=1699364612~hmac=ff52de907dd32cb7cc25828f5807cc65909670cdc661c8385175737605f1470f", Url = "https://teletype.in/@repetitme/online_lessons", Text = "Как проводить онлайн - занятия ?", ColorText = "#FFFFFF" },
                new TeacherUseFulUrl { Id = 9, Image = "https://img.freepik.com/free-psd/question-mark-speech-bubble-icon-isolated-3d-render-illustration_47987-11661.jpg?w=1060&t=st=1699364032~exp=1699364632~hmac=b9780246b63790d28062a5a78a4a454a15ba74ee89136a9edd3f33f6d54560db", Url = "https://teletype.in/@repetitme/faq_tutors", Text = "FAQ - вопросырепетиторов", ColorText = "#000000" }
            );

            /*modelBuilder.Entity<Teacher>().HasData(
                new Teacher { Id = 1, UserId = 2, PaymentRating = 700, Rating = 5.0, Image = "testteacher", StatusId = 5, ScienceId = 2, LessonTargetId = , AgeCategoryId  = 3, Experience = 2, Price = 2500, Visibility = true},
                new Teacher { Id = 2, UserId = 3, PaymentRating = 1000, Rating = 4.4, Image = "testteacher2", StatusId = 2, ScienceId = 1, LessonTargetId = 3, AgeCategoryId = 4, Experience = 1, Price = 1200, Visibility = false}
                );*/

            /* modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, UserId = 1},
                new Student { Id = 2, UserId = 3}
                );*/

            /*modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Сергей", TelegramName = "@Serg" ,TelegramId = 12, LastActivity = 1, Block = false },
                new User { Id = 2, Name = "Владимир", TelegramName = "@Vlad", SecondName = "Петров", TelegramId = 23, LastActivity = 2, Block = false },
                new User { Id = 3, Name = "Даниил", TelegramName = "@Dania", SecondName = "Семенов", TelegramId = 34, LastActivity = 1 , Block = false }
                );*/
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
