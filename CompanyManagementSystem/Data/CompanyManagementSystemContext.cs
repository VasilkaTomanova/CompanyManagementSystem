using CompanyManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagementSystem.Data
{
    public class CompanyManagementSystemContext : DbContext
    {
        public CompanyManagementSystemContext()
        {

        }
        public CompanyManagementSystemContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeesProjects> EmployeesProjects { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Project> Projects { get; set; }

        //TODO
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<EmployeesProjects>()
                .HasKey(x => new { x.EmployeeId, x.ProjectId });

            builder.Entity<EmployeesProjects>()
    .HasOne(e => e.Project)
    .WithMany(s => s.Employees)
    .HasForeignKey(e => e.ProjectId);


            builder.Entity<EmployeesProjects>()
    .HasOne(e => e.Employee)
    .WithMany(s => s.Projects)
    .HasForeignKey(e => e.EmployeeId);

        }




    }
}
