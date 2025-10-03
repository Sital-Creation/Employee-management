using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Api.Models
{
    [Table("department")] 
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        [Required]
        [MaxLength(100)]
        public string DepartmentName { get; set; }

        public string? Description { get; set; }

        
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
