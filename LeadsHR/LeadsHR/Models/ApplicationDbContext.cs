using LeadsHR.Models;
using Microsoft.EntityFrameworkCore;


namespace LeadsHR.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EducationInfo> EducationInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Employees
            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, FirstName = "John", LastName = "Doe", Division = "HR", Email= "johndoe@gmail.com", Building = "A", Title = "Manager", Room = "101" },
                new Employee { EmployeeId = 2, FirstName = "Jane", LastName = "Smith", Division = "Finance", Email = "janesmith@gmail.com", Building = "B", Title = "Analyst", Room = "202" },
                new Employee { EmployeeId = 3, FirstName = "Michael", LastName = "Brown", Division = "IT", Email = "michaelbrown@gmail.com", Building = "C", Title = "Developer", Room = "303" },
                new Employee { EmployeeId = 4, FirstName = "Emily", LastName = "Clark", Division = "Marketing", Email = "emilyclark@gmail.com", Building = "D", Title = "Executive", Room = "404" },
                new Employee { EmployeeId = 5, FirstName = "Daniel", LastName = "Wilson", Division = "Sales", Email = "danielwilson@gmail.com", Building = "E", Title = "Representative", Room = "505" }
            );

            // Seed Education Information
            modelBuilder.Entity<EducationInfo>().HasData(
                new EducationInfo { EducationInfoId = 1, EmployeeId = 1, Degree = "MBA", Institution = "Harvard", YearOfPassing = 2015, CGPA = "3.50" },
                new EducationInfo { EducationInfoId = 2, EmployeeId = 2, Degree = "BBA", Institution = "Stanford", YearOfPassing = 2018, CGPA = "3.80" },
                new EducationInfo { EducationInfoId = 3, EmployeeId = 3, Degree = "BSc Computer Science", Institution = "MIT", YearOfPassing = 2020, CGPA = "3.90" },
                new EducationInfo { EducationInfoId = 4, EmployeeId = 4, Degree = "MSc Marketing", Institution = "Oxford", YearOfPassing = 2016, CGPA = "3.70" },
                new EducationInfo { EducationInfoId = 5, EmployeeId = 5, Degree = "BSc Economics", Institution = "Cambridge", YearOfPassing = 2019, CGPA = "3.85" }

            );
        }
    }
}
