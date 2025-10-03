using Employee_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Api.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
        }

        // Tables (DbSets)
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<User> Users { get; set; }   // ✅ Users table add karo

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Employee → Department (1-to-many)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentID);

            //// Salary → Employee (1-to-1)
            //modelBuilder.Entity<Salary>()
            //    .HasOne(s => s.Employee)
            //    .WithOne(s => s.Salary)
            //    .HasForeignKey<Salary>(s => s.EmployeeID);

            // ✅ MySQL Computed Column (remove [])
            modelBuilder.Entity<Salary>()
                .Property(s => s.NetSalary)
                .ValueGeneratedOnAddOrUpdate()
                .HasComputedColumnSql("Basic + Allowances - Deductions");

            modelBuilder.Entity<Salary>()
                   .HasOne(s => s.Employee)
                   .WithOne(e => e.SalaryDetails)
                   .HasForeignKey<Salary>(s => s.EmployeeID);

            modelBuilder.Entity<Salary>().ToTable("salary"); 
            modelBuilder.Entity<User>().ToTable("users");
        }
    }
}
