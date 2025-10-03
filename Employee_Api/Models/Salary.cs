namespace Employee_Api.Models
{
    public class Salary
    {
        public int SalaryID { get; set; }
        public int EmployeeID { get; set; }
        public decimal Basic { get; set; }
        public decimal Allowances { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetSalary { get; set; }
        // Navigation property
        public Employee? Employee { get; set; }
    }
}
