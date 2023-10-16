using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepititMe.Domain.Entities.Data;

namespace RepititMe.Domain.Entities.Users
{
    public class User
    {
        public int Id { get; set; }
        public int TelegramId { get; set; }
        public int LastActivity { get; set; }
        public string Name { get; set; }
        public string? SecondName { get; set; }
    }
}
