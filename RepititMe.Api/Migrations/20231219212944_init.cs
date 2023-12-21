using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RepititMe.Api.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgeCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LessonTargets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonTargets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    PaymentId = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sciences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sciences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentUseFulUrls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    ColorText = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentUseFulUrls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeacherStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeacherUseFulUrls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    ColorText = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherUseFulUrls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TelegramId = table.Column<long>(type: "bigint", nullable: false),
                    TelegramName = table.Column<string>(type: "text", nullable: false),
                    LastActivity = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SecondName = table.Column<string>(type: "text", nullable: true),
                    Block = table.Column<bool>(type: "boolean", nullable: false),
                    Admin = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScienceLessonTargets",
                columns: table => new
                {
                    ScienceId = table.Column<int>(type: "integer", nullable: false),
                    LessonTargetId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScienceLessonTargets", x => new { x.ScienceId, x.LessonTargetId });
                    table.ForeignKey(
                        name: "FK_ScienceLessonTargets_LessonTargets_LessonTargetId",
                        column: x => x.LessonTargetId,
                        principalTable: "LessonTargets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScienceLessonTargets_Sciences_ScienceId",
                        column: x => x.ScienceId,
                        principalTable: "Sciences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: true),
                    StatusId = table.Column<int>(type: "integer", nullable: true),
                    ScienceId = table.Column<int>(type: "integer", nullable: true),
                    Experience = table.Column<int>(type: "integer", nullable: true),
                    AboutMe = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<int>(type: "integer", nullable: true),
                    VideoPresentation = table.Column<string>(type: "text", nullable: true),
                    Certificates = table.Column<List<string>>(type: "text[]", nullable: true),
                    Visibility = table.Column<bool>(type: "boolean", nullable: false),
                    Rating = table.Column<double>(type: "double precision", nullable: false),
                    PaymentRating = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_Sciences_ScienceId",
                        column: x => x.ScienceId,
                        principalTable: "Sciences",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Teachers_TeacherStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "TeacherStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Teachers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Disputes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TeacherId = table.Column<int>(type: "integer", nullable: false),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    AcceptFromTeacher = table.Column<bool>(type: "boolean", nullable: true),
                    PriceTeacher = table.Column<double>(type: "double precision", nullable: true),
                    DataFromTeacher = table.Column<string>(type: "text", nullable: true),
                    AcceptFromStudent = table.Column<bool>(type: "boolean", nullable: true),
                    PriceStudent = table.Column<double>(type: "double precision", nullable: true),
                    DataFromStudent = table.Column<string>(type: "text", nullable: true),
                    StatusClose = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disputes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disputes_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Disputes_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TeacherId = table.Column<int>(type: "integer", nullable: false),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    DateTimeAccept = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateTimeFirstLesson = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    RefusedStudent = table.Column<bool>(type: "boolean", nullable: false),
                    RefusedTeacher = table.Column<bool>(type: "boolean", nullable: false),
                    Commission = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TeacherId = table.Column<int>(type: "integer", nullable: false),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Rating = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherAgeCategorys",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "integer", nullable: false),
                    AgeCategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherAgeCategorys", x => new { x.TeacherId, x.AgeCategoryId });
                    table.ForeignKey(
                        name: "FK_TeacherAgeCategorys_AgeCategories_AgeCategoryId",
                        column: x => x.AgeCategoryId,
                        principalTable: "AgeCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherAgeCategorys_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherLessonTargets",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "integer", nullable: false),
                    LessonTargetId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherLessonTargets", x => new { x.TeacherId, x.LessonTargetId });
                    table.ForeignKey(
                        name: "FK_TeacherLessonTargets_LessonTargets_LessonTargetId",
                        column: x => x.LessonTargetId,
                        principalTable: "LessonTargets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherLessonTargets_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveisFirst",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    StudentAnswer = table.Column<bool>(type: "boolean", nullable: false),
                    TelegramIdStudent = table.Column<long>(type: "bigint", nullable: false),
                    StudentAccept = table.Column<bool>(type: "boolean", nullable: true),
                    StudentPrice = table.Column<double>(type: "double precision", nullable: true),
                    StudentWhy = table.Column<string>(type: "text", nullable: true),
                    TeacherAnswer = table.Column<bool>(type: "boolean", nullable: false),
                    TelegramIdTeacher = table.Column<long>(type: "bigint", nullable: false),
                    TeacherAccept = table.Column<bool>(type: "boolean", nullable: true),
                    TeacherPrice = table.Column<double>(type: "double precision", nullable: true),
                    TeacherCause = table.Column<string>(type: "text", nullable: true),
                    RepitSurveyTeacher = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TeacherSpecify = table.Column<string>(type: "text", nullable: true),
                    TeacherWhy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveisFirst", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveisFirst_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveisSecond",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    StudentAnswer = table.Column<bool>(type: "boolean", nullable: false),
                    TelegramIdStudent = table.Column<long>(type: "bigint", nullable: false),
                    StudentAccept = table.Column<bool>(type: "boolean", nullable: true),
                    StudentWannaNext = table.Column<bool>(type: "boolean", nullable: true),
                    StudentCancel = table.Column<bool>(type: "boolean", nullable: true),
                    StudentWhy = table.Column<string>(type: "text", nullable: true),
                    RepitSurveyStudent = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TeacherAnswer = table.Column<bool>(type: "boolean", nullable: false),
                    TelegramIdTeacher = table.Column<long>(type: "bigint", nullable: false),
                    TeacherAccept = table.Column<bool>(type: "boolean", nullable: true),
                    TeacherWannaNext = table.Column<bool>(type: "boolean", nullable: true),
                    TeacherCancel = table.Column<bool>(type: "boolean", nullable: true),
                    TeacherCause = table.Column<string>(type: "text", nullable: true),
                    TeacherSpecify = table.Column<string>(type: "text", nullable: true),
                    TeacherWhy = table.Column<string>(type: "text", nullable: true),
                    RepitSurveyTeacher = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveisSecond", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveisSecond_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AgeCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Дошкольники: 4-7 лет" },
                    { 2, "Начальные классы: 1-4 класс" },
                    { 3, "Средние классы: 5-8 класс" },
                    { 4, "Старшие классы: 9-11 класс" },
                    { 5, "Студенты" },
                    { 6, "Взрослые" }
                });

            migrationBuilder.InsertData(
                table: "LessonTargets",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "ОГЭ по математике" },
                    { 2, "ЕГЭ по математике (базовый уровень)" },
                    { 3, "ЕГЭ по математике (профильный уровень)" },
                    { 4, "Подготовка к олимпиаде по математике" },
                    { 5, "ДВИ по математике" },
                    { 6, "ВПР по математике" },
                    { 7, "Подготовка к экзамену в вузе" },
                    { 8, "Повышение успеваемости" },
                    { 9, "Для себя" },
                    { 10, "Подготовка к олимпиаде по английскому языку" },
                    { 11, "ОГЭ по английскому языку" },
                    { 12, "ЕГЭ по английскому языку" },
                    { 13, "ДВИ по английскому языку" },
                    { 14, "ВПР по английскому языку" },
                    { 15, "Подготовка к олимпиаде по истории" },
                    { 16, "ОГЭ по истории" },
                    { 17, "ЕГЭ по истории" },
                    { 18, "ДВИ по истории" },
                    { 19, "ВПР по истории" },
                    { 20, "Подготовка к олимпиаде по информатике" },
                    { 21, "ОГЭ по информатике" },
                    { 22, "ЕГЭ по информатике" },
                    { 23, "ДВИ по информатике" },
                    { 24, "ВПР по информатике" },
                    { 25, "Подготовка к олимпиаде по русскому языку" },
                    { 26, "ОГЭ по русскому языку" },
                    { 27, "ЕГЭ по русскому языку" },
                    { 28, "ДВИ по русскому языку" },
                    { 29, "ВПР по русскому языку" },
                    { 30, "Подготовка к олимпиаде по физике" },
                    { 31, "ОГЭ по физике" },
                    { 32, "ЕГЭ по физике" },
                    { 33, "ДВИ по физике" },
                    { 34, "ВПР по физике" },
                    { 35, "Подготовка к олимпиаде по химии" },
                    { 36, "ОГЭ по химии" },
                    { 37, "ЕГЭ по химии" },
                    { 38, "ДВИ по химии" },
                    { 39, "ВПР по химии" },
                    { 40, "Подготовка к олимпиаде по биологии" },
                    { 41, "ОГЭ по биологии" },
                    { 42, "ЕГЭ по биологии" },
                    { 43, "ДВИ по биологии" },
                    { 44, "ВПР по биологии" },
                    { 45, "Подготовка к олимпиаде по обществознанию" },
                    { 46, "ОГЭ по обществознанию" },
                    { 47, "ЕГЭ по обществознанию" },
                    { 48, "ДВИ по обществознанию" },
                    { 49, "ВПР по обществознанию" }
                });

            migrationBuilder.InsertData(
                table: "Sciences",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Математика" },
                    { 2, "Английский язык" },
                    { 3, "История" },
                    { 4, "Информатика" },
                    { 5, "Русский язык" },
                    { 6, "Физика" },
                    { 7, "Химия" },
                    { 8, "Биология" },
                    { 9, "Обществознание" }
                });

            migrationBuilder.InsertData(
                table: "StudentUseFulUrls",
                columns: new[] { "Id", "ColorText", "Image", "Text", "Url" },
                values: new object[,]
                {
                    { 1, "#FFFFFF", "https://img.freepik.com/free-photo/representations-user-experience-interface-design_23-2150104504.jpg?w=1380&t=st=1699363487~exp=1699364087~hmac=a9b760c1990278d66e9109276b979b59e76c80ca595013e73eba0940c56d26cc", "Как пользоваться сервисом? ", "https://teletype.in/@repetitme/about_us" },
                    { 2, "#000000", "https://img.freepik.com/free-photo/golden-correct-sign-best-quality-assurance-guarantee-product-iso-service-concept_616485-97.jpg?w=1380&t=st=1699363513~exp=1699364113~hmac=ecaf72d45234a4bb1219570234da5ceabf78e3bf10a97af84e00afb49fcd5ae5", "Преимущества сервиса для клиентов", "https://teletype.in/@repetitme/adv_for_students" },
                    { 3, "#FFFFFF", "https://img.freepik.com/free-photo/beautiful-rendering-dating-app-concept_23-2149316416.jpg?w=1060&t=st=1699363774~exp=1699364374~hmac=e63b5200fc721a3e448eba417175666be7775c2c0f33da70ee967308a2d0240d", "Способы подобрать репетитора", "https://teletype.in/@repetitme/ways_to_find_tutors" },
                    { 4, "#FFFFFF", "https://img.freepik.com/free-photo/comment-message-inbox-shape-social-media-notification-icon-speech-bubbles-3d-cartoon-banner-website-ui-pink-background-3d-rendering-illustration_56104-1328.jpg?w=996&t=st=1699363791~exp=1699364391~hmac=70384b0d70615256dca861d55637a99b8eeac5a381ef02c0e7768b8de466552e", "Обратная связь от ученика", "https://teletype.in/@repetitme/feedback_students" },
                    { 5, "#000000", "https://img.freepik.com/free-photo/3d-render-customer-leave-feedback-phone-screen_107791-17460.jpg?w=826&t=st=1699363813~exp=1699364413~hmac=676797ff3134036292b7b0678a890743998fe3ede313c132a1e5fcbd935c6b06", "Как проводить онлайн-занятия?", "https://teletype.in/@repetitme/online_lessons" },
                    { 6, "#000000", "https://img.freepik.com/free-photo/3d-render-online-education-survey-test-concept_107791-15665.jpg?w=1800&t=st=1699363835~exp=1699364435~hmac=1dcdc60cb6f2e8d2e2362d1ff8101f78c0e7f41f79308f22fb2fc3d7185c7986", "Как оставить отзыв о репетиторе", "https://teletype.in/@repetitme/review" },
                    { 7, "#000000", "https://img.freepik.com/free-psd/question-mark-confirmed-signs-false-rejection-icon-3d-render-isolated_47987-11659.jpg?w=1060&t=st=1699363851~exp=1699364451~hmac=f1f533e4f73684627235426e2fbc4992512f9f71b2be48ef10a6adc8a5c1bdcc", "FAQ– вопросы учеников", "https://teletype.in/@repetitme/faq_students" }
                });

            migrationBuilder.InsertData(
                table: "TeacherStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Студент" },
                    { 2, "Аспирант" },
                    { 3, "Частный преподаватель" },
                    { 4, "Школьный преподаватель" },
                    { 5, "Профессор" }
                });

            migrationBuilder.InsertData(
                table: "TeacherUseFulUrls",
                columns: new[] { "Id", "ColorText", "Image", "Text", "Url" },
                values: new object[,]
                {
                    { 1, "#000000", "https://img.freepik.com/free-photo/representations-user-experience-interface-design_23-2150104504.jpg?w=1380&t=st=1699363877~exp=1699364477~hmac=1af63fd6e634f7117b35f5828ab2438586596ff114107c04372bb7933069f072", "Как пользоваться сервисом?", "https://teletype.in/@repetitme/about_us" },
                    { 2, "#FFFFFF", "https://img.freepik.com/free-photo/3d-render-bullseye-target-sandglass-coins_107791-16200.jpg?w=1060&t=st=1699363889~exp=1699364489~hmac=f3fb45411408c5c52ae24de4ac5c71cbaa9b5c0300a7d274eb52ad3c557505df", "Преимущества сервиса для репетиторов", "https://teletype.in/@repetitme/adv_for_tutors" },
                    { 3, "#FFFFFF", "https://img.freepik.com/free-photo/mobile-phone-with-check-list-screen_107791-17459.jpg?w=826&t=st=1699363905~exp=1699364505~hmac=b63d3159070c03c0782365fce92d943e06467d15a5540fe32f3a00c9b03c085f", "Анкета репетитора", "https://teletype.in/@repetitme/from_for_tutors" },
                    { 4, "#FFFFFF", "https://img.freepik.com/free-photo/golden-russian-ruble-coins-currency-money-sign-symbol-background-3d-illustration_56104-1707.jpg?w=740&t=st=1699363923~exp=1699364523~hmac=71e34623397ca15b7bc078617e449936286fda33eba6809317e99b58159875ed", "Как формируется комиссия за заявки?", "https://teletype.in/@repetitme/about_fee" },
                    { 5, "#FFFFFF", "https://img.freepik.com/free-photo/five-golden-stars-client-excellent-evaluation-after-use-product-service-concept-by-3d-render_616485-15.jpg?w=1380&t=st=1699363939~exp=1699364539~hmac=c3a33800b559a6ad51484a74095b2863a0c0b459dfc9a3384c2ab4410016c8c6", "Как формируется рейтинг?", "https://teletype.in/@repetitme/rating_of_tutor" },
                    { 6, "#FFFFFF", "https://img.freepik.com/free-photo/3d-rendering-social-media-icon_23-2150701008.jpg?w=1060&t=st=1699363955~exp=1699364555~hmac=3c1c1242d415454fc889dd2154994aa05bbfdb971d8b1de943be29e7967d3f9c", "Уведомления для репетитора", "https://teletype.in/@repetitme/feedback_tutors" },
                    { 7, "#FFFFFF", "https://img.freepik.com/free-photo/3d-render-customer-leave-feedback-phone-screen_107791-17460.jpg?w=826&t=st=1699363994~exp=1699364594~hmac=835f40a2ce752e0d61c0d54582e463a8705394efae80722e2310a6a6b45b4746", "Как оставить отзыв о репетиторе", "https://teletype.in/@repetitme/review" },
                    { 8, "#FFFFFF", "https://img.freepik.com/free-photo/3d-render-online-education-survey-test-concept_107791-15665.jpg?w=1800&t=st=1699364012~exp=1699364612~hmac=ff52de907dd32cb7cc25828f5807cc65909670cdc661c8385175737605f1470f", "Как проводить онлайн - занятия ?", "https://teletype.in/@repetitme/online_lessons" },
                    { 9, "#000000", "https://img.freepik.com/free-psd/question-mark-speech-bubble-icon-isolated-3d-render-illustration_47987-11661.jpg?w=1060&t=st=1699364032~exp=1699364632~hmac=b9780246b63790d28062a5a78a4a454a15ba74ee89136a9edd3f33f6d54560db", "FAQ - вопросырепетиторов", "https://teletype.in/@repetitme/faq_tutors" }
                });

            migrationBuilder.InsertData(
                table: "ScienceLessonTargets",
                columns: new[] { "LessonTargetId", "ScienceId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 },
                    { 7, 1 },
                    { 8, 1 },
                    { 9, 1 },
                    { 7, 2 },
                    { 8, 2 },
                    { 9, 2 },
                    { 10, 2 },
                    { 11, 2 },
                    { 12, 2 },
                    { 13, 2 },
                    { 14, 2 },
                    { 7, 3 },
                    { 8, 3 },
                    { 9, 3 },
                    { 15, 3 },
                    { 16, 3 },
                    { 17, 3 },
                    { 18, 3 },
                    { 19, 3 },
                    { 7, 4 },
                    { 8, 4 },
                    { 9, 4 },
                    { 20, 4 },
                    { 21, 4 },
                    { 22, 4 },
                    { 23, 4 },
                    { 24, 4 },
                    { 7, 5 },
                    { 8, 5 },
                    { 9, 5 },
                    { 25, 5 },
                    { 26, 5 },
                    { 27, 5 },
                    { 28, 5 },
                    { 29, 5 },
                    { 7, 6 },
                    { 8, 6 },
                    { 9, 6 },
                    { 30, 6 },
                    { 31, 6 },
                    { 32, 6 },
                    { 33, 6 },
                    { 34, 6 },
                    { 7, 7 },
                    { 8, 7 },
                    { 9, 7 },
                    { 35, 7 },
                    { 36, 7 },
                    { 37, 7 },
                    { 38, 7 },
                    { 39, 7 },
                    { 7, 8 },
                    { 8, 8 },
                    { 9, 8 },
                    { 40, 8 },
                    { 41, 8 },
                    { 42, 8 },
                    { 43, 8 },
                    { 44, 8 },
                    { 7, 9 },
                    { 8, 9 },
                    { 9, 9 },
                    { 45, 9 },
                    { 46, 9 },
                    { 47, 9 },
                    { 48, 9 },
                    { 49, 9 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Disputes_StudentId",
                table: "Disputes",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Disputes_TeacherId",
                table: "Disputes",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StudentId",
                table: "Orders",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TeacherId",
                table: "Orders",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_OrderId",
                table: "Reports",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_StudentId",
                table: "Reviews",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_TeacherId",
                table: "Reviews",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_ScienceLessonTargets_LessonTargetId",
                table: "ScienceLessonTargets",
                column: "LessonTargetId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                table: "Students",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveisFirst_OrderId",
                table: "SurveisFirst",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveisSecond_OrderId",
                table: "SurveisSecond",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAgeCategorys_AgeCategoryId",
                table: "TeacherAgeCategorys",
                column: "AgeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherLessonTargets_LessonTargetId",
                table: "TeacherLessonTargets",
                column: "LessonTargetId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_ScienceId",
                table: "Teachers",
                column: "ScienceId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_StatusId",
                table: "Teachers",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_UserId",
                table: "Teachers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Disputes");

            migrationBuilder.DropTable(
                name: "PaymentStatuses");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "ScienceLessonTargets");

            migrationBuilder.DropTable(
                name: "StudentUseFulUrls");

            migrationBuilder.DropTable(
                name: "SurveisFirst");

            migrationBuilder.DropTable(
                name: "SurveisSecond");

            migrationBuilder.DropTable(
                name: "TeacherAgeCategorys");

            migrationBuilder.DropTable(
                name: "TeacherLessonTargets");

            migrationBuilder.DropTable(
                name: "TeacherUseFulUrls");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "AgeCategories");

            migrationBuilder.DropTable(
                name: "LessonTargets");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Sciences");

            migrationBuilder.DropTable(
                name: "TeacherStatuses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
