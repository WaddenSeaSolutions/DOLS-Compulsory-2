using Microsoft.EntityFrameworkCore;
using DOLS.UserMicroService.Models;

namespace DOLS.UserMicroService.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}