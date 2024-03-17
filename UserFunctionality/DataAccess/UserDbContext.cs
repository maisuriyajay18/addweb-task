using Microsoft.EntityFrameworkCore;
using UserFunctionality.Models;

namespace UserFunctionality.DataAccess
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Users> users { get; set; }
    }
}
