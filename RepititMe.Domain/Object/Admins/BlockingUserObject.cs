using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Object.Admins
{
    public class BlockingUserObject
    {
        public long TelegramIdAdmin { get; set; }
        public long TelegramIdUser { get; set; }
    }
}
