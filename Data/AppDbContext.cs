using Microsoft.EntityFrameworkCore;
using ToDoApplication.Models;

namespace ToDoApplication.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Task> Tasks { get; set; }
    }
}