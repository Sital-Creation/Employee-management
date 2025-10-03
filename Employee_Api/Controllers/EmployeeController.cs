using Employee_Api.Data;
using Employee_Api.DTOs;
using Employee_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        public EmployeeController(EmployeeDbContext context)
        {
            _context = context;
        }

        // GET: api/employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            try
            {
                var employees = await _context.Employees
                    .Include(e => e.Department)
                    .Select(e => new EmployeeDTO
                    {
                        EmployeeID = e.EmployeeID,
                        Name = e.Name,
                        Email = e.Email,
                        Phone = e.Phone,
                        DepartmentID = e.DepartmentID,
                        DepartmentName = e.Department.DepartmentName,
                        Position = e.Position,
                        BaseSalary = e.BaseSalary,
                        DateOfJoining = e.DateOfJoining,
                        Status = e.Status
                    })
                    .ToListAsync();

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(int id)
        {
            try
            {
                var employee = await _context.Employees
                    .Include(e => e.Department)
                    .Where(e => e.EmployeeID == id)
                    .Select(e => new EmployeeDTO
                    {
                        EmployeeID = e.EmployeeID,
                        Name = e.Name,
                        Email = e.Email,
                        Phone = e.Phone,
                        DepartmentID = e.DepartmentID,
                        DepartmentName = e.Department.DepartmentName,
                        Position = e.Position,
                        BaseSalary = e.BaseSalary,
                        DateOfJoining = e.DateOfJoining,
                        Status = e.Status
                    })
                    .FirstOrDefaultAsync();

                if (employee == null) return NotFound("Employee not found");

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/employee
        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> CreateEmployee([FromBody] EmployeeDTO dto)
        {
            try
            {
                var employee = new Employee
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    DepartmentID = dto.DepartmentID,
                    Position = dto.Position,
                    BaseSalary = dto.BaseSalary,
                    DateOfJoining = dto.DateOfJoining,
                    Status = dto.Status
                };

                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();

                dto.EmployeeID = employee.EmployeeID;
                return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeID }, dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeDTO dto)
        {
            if (id != dto.EmployeeID) return BadRequest("ID mismatch");

            try
            {
                var employee = await _context.Employees.FindAsync(id);
                if (employee == null) return NotFound("Employee not found");

                employee.Name = dto.Name;
                employee.Email = dto.Email;
                employee.Phone = dto.Phone;
                employee.DepartmentID = dto.DepartmentID;
                employee.Position = dto.Position;
                employee.BaseSalary = dto.BaseSalary;
                employee.DateOfJoining = dto.DateOfJoining;
                employee.Status = dto.Status;

                _context.Entry(employee).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id);
                if (employee == null) return NotFound("Employee not found");

                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();

                return Ok("Employee deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
