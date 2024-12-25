using Crud_MVC_EF.Models;
using Microsoft.EntityFrameworkCore;


namespace Crud_MVC_EF.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options): base(options)
        {

        }
        public DbSet<Student> students { get; set; }
        public DbSet<User> users { get; set; }

        public DbSet<Country> countries { get; set; }
        public DbSet<City> cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}
