using Microsoft.AspNetCore.Http;

namespace RepititMe.Domain.Object.Users
{
    public class UserSignUpTeacherObject
    {
        public int TelegramId { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
    }
}
