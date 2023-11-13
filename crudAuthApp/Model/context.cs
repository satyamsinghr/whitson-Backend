using Microsoft.EntityFrameworkCore;

namespace crudAuthApp.Model
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public DbSet<Info> Infos { get; set; }
        
        public DbSet<UserDetail> UserDetails { get; set; }
        
        public DbSet<EmailDetail> EmailDetails { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Manager> Manager { get; set; }
    }
}
