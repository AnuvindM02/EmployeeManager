using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Entities
{
    public class EmployeesDbContext : DbContext
    {
        public EmployeesDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<Employee> Employees { get; set;}
        public DbSet<EmployeeTask> EmployeeTasks { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Creating the tables
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<EmployeeTask>().ToTable("EmployeeTasks");

            //Seeding employees data
            string employeesJson = System.IO.File.ReadAllText("EmployeesData.json");
            List<Employee> employees = JsonSerializer.Deserialize<List<Employee>>(employeesJson);
            foreach (Employee employee in employees)
            {
                modelBuilder.Entity<Employee>().HasData(employee);
            }

            //Seeding employee tasks data
            string employeeTasksJson = System.IO.File.ReadAllText("EmployeeTasksData.json");
            List<EmployeeTask> employeeTasks = JsonSerializer.Deserialize<List<EmployeeTask>>(employeeTasksJson);
            foreach (EmployeeTask employeeTask in employeeTasks)
            {
                modelBuilder.Entity<EmployeeTask>().HasData(employeeTask);
            }


        }
    }
}
