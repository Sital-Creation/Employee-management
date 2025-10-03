namespace Employee_Api.DTOs
{
    public class SalaryDTO
    {
        public int SalaryID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public decimal Basic { get; set; }
        public decimal Allowances { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetSalary { get; set; }
    }
}
