using Microsoft.EntityFrameworkCore;
using ProjectDBManager.Models.Data.Entities;

namespace ProjectDBManager.Models.Data
{
    public class EDBContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ProjectEmployee> ProjectEmployee { get; set; }

        // Constructor taking DbContextOptions as parameter
        public EDBContext(DbContextOptions<EDBContext> options) : base(options)
        {
        }

        // Configuring relationships between entities
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define composite primary key for ProjectEmployee table
            modelBuilder.Entity<ProjectEmployee>()
                .HasKey(pe => new { pe.ProjectId, pe.EmployeeId });

            // Define relationship between Project and ProjectEmployee
            modelBuilder.Entity<ProjectEmployee>()
                .HasOne(pe => pe.Project)
                .WithMany(p => p.ProjectEmployees)
                .HasForeignKey(pe => pe.ProjectId);

            // Define relationship between Employee and ProjectEmployee
            modelBuilder.Entity<ProjectEmployee>()
                .HasOne(pe => pe.Employee)
                .WithMany(e => e.ProjectEmployees)
                .HasForeignKey(pe => pe.EmployeeId);

            modelBuilder.Entity<Project>().HasData(

          new Project
          {
              ProjectId = 1,
              Name = "  Sybers",
              CustomerCompanyName = "Sber",
              ExecutorCompanyName = "Galera",
              StartDate = new DateTime(2024, 1, 1),
              EndDate = new DateTime(2024, 12, 31),
              DirectorId = 1,
              Priority = 1
          },
          new Project
          {
              ProjectId = 2,
              Name = "Uralski Pelmeni",
              CustomerCompanyName = "Sber",
              ExecutorCompanyName = "IDK",
              StartDate = new DateTime(2024, 2, 1),
              EndDate = new DateTime(2024, 11, 30),
              DirectorId = 2,
              Priority = 2
          },

           new Project
           {
               ProjectId = 3,
               Name = "Amazon",
               CustomerCompanyName = "Ozon",
               ExecutorCompanyName = "Wildberries",
               StartDate = new DateTime(2024, 2, 1),
               EndDate = new DateTime(2024, 11, 30),
               DirectorId = 3,
               Priority = 3
           }
           );



            modelBuilder.Entity<Employee>().HasData(
   new Employee { EmployeeId = 1, FirstName = "Sasha", LastName = "Kosinova", MiddleName = "Igorevna", Email = "sk464656@gmail.com" },
   new Employee { EmployeeId = 2, FirstName = "Ivan", LastName = "Popov", MiddleName = "Sergeevich", Email = "vanya@example.com" },
   new Employee { EmployeeId = 3, FirstName = "Boris", LastName = "Cherniy", MiddleName = "Petrovich", Email = "borya@example.com" }
);


            modelBuilder.Entity<ProjectEmployee>().HasData(
                new ProjectEmployee { ProjectId = 1, EmployeeId = 1 },
                new ProjectEmployee { ProjectId = 2, EmployeeId = 2 },
                 new ProjectEmployee { ProjectId = 3, EmployeeId = 3 }
            );
        }
    }
}
