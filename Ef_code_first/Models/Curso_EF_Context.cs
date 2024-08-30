using Ef_code_first.Migrations;
using Microsoft.EntityFrameworkCore;

namespace Ef_code_first.Models
{
    public class Curso_EF_Context : DbContext
    {

        private readonly IConfiguration _configuration;

        public Curso_EF_Context(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<WorkingExperience> workingExperiences { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("cs");
                optionsBuilder.UseMySQL(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           //Direct  data loading
            /*modelBuilder.Entity<User>().HasData(
                new User() { Email = "user1@gmail.com", Id = 1, Name = "name1" },
                new User() { Email = "user2@gmail.com", Id = 2, Name = "name2" });*/

            //Good practices!!
            //Creating a seed for each class (table)!
            //Each time the seeds are modified, a new migration must be created (dotnet ef migration add [name of the new migration]
            modelBuilder.ApplyConfiguration(new UserSeed());
        }


    }
}
