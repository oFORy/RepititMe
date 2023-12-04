using Microsoft.AspNetCore.Http;

namespace RepititMe.Domain.Object.Users
{
    public class ChangeProfileTeacherObject
    {
        public long TelegramId { get; set; }
        public IFormFile? Image { get; set; }
        public int StatusId { get; set; }
        public int ScienceId { get; set; }
        public List<int> LessonTargetId { get; set; }
        public List<int> AgeCategoryId { get; set; }
        public int? Experience { get; set; }
        public string? AboutMe { get; set; }
        public int Price { get; set; }
        public List<IFormFile>? VideoPresentation { get; set; }
        public List<IFormFile>? Certificates { get; set; }
    }
}
