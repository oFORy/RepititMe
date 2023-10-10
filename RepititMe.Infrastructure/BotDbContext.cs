using Microsoft.EntityFrameworkCore;
using RepititMe.Domain.Entities;

namespace RepititMe.Infrastructure
{
    public class BotDbContext : DbContext
    {
        public BotDbContext(DbContextOptions<BotDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


        }



    }
}
