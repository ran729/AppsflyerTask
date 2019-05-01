using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AppsflyerTwitter.DAL
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Tweet> Tweets { get; set; }

        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
    }
}
