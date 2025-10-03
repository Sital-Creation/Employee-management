namespace Employee_Api.DTOs
{
    public class EmployeeDTO
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string Position { get; set; }
        public decimal BaseSalary { get; set; } // original salary value
        public DateTime DateOfJoining { get; set; }
        public string Status { get; set; }
    }
}
