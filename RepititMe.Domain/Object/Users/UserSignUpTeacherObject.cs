using Microsoft.AspNetCore.Http;

namespace RepititMe.Domain.Object.Users
{
    public class UserSignUpTeacherObject
    {
        public long TelegramId { get; set; }
        public string TelegramName { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
    }
}
