using Microsoft.EntityFrameworkCore;
using project1.Models;

namespace project1.Context
{
    public class TodoContext:DbContext
    {
        public DbSet<Todo> todos { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<User> users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=SMART\\SQLEXPRESS;Database=Todo;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
