using LivegramBot.Entities;
using Microsoft.EntityFrameworkCore;

namespace LivegramBot.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        string connectionString = "Server = localhost; Port=5432;User Id = postgres; Password=1234;DataBase=Livegram.bot;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(connectionString);
        }

        public DbSet<User> User { get; set; }
    }
}