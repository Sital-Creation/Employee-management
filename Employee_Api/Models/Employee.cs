using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Api.Models
{
    [Table("employee")]  // 👈 exact table name from MySQL
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        public int? DepartmentID { get; set; }

        [MaxLength(100)]
        public string? Position { get; set; }

        [Column("Salary")]  // 👈 maps BaseSalary → Salary column
        public decimal BaseSalary { get; set; }

        public DateTime DateOfJoining { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Active";

        // Navigation properties
        public Department? Department { get; set; }
        public Salary? SalaryDetails { get; set; }
    }
}
