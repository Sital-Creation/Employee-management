using Employee_Api.Data;
using Employee_Api.DTOs;
using Employee_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        public DepartmentController(EmployeeDbContext context)
        {
            _context = context;
        }

        // GET: api/department
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetDepartments()
        {
            try
            {
                var departments = await _context.Departments
                    .Select(d => new DepartmentDTO
                    {
                        DepartmentID = d.DepartmentID,
                        DepartmentName = d.DepartmentName,
                        Description = d.Description
                    })
                    .ToListAsync();

                return Ok(departments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/department/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDTO>> GetDepartment(int id)
        {
            try
            {
                var department = await _context.Departments.FindAsync(id);
                if (department == null) return NotFound();

                return Ok(new DepartmentDTO
                {
                    DepartmentID = department.DepartmentID,
                    DepartmentName = department.DepartmentName,
                    Description = department.Description
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/department
        [HttpPost]
        public async Task<ActionResult<DepartmentDTO>> CreateDepartment([FromBody] DepartmentDTO dto)
        {
            try
            {
                var department = new Department
                {
                    DepartmentName = dto.DepartmentName,
                    Description = dto.Description
                };

                _context.Departments.Add(department);
                await _context.SaveChangesAsync();

                dto.DepartmentID = department.DepartmentID;
                return CreatedAtAction(nameof(GetDepartment), new { id = department.DepartmentID }, dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/department/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentDTO dto)
        {
            if (id != dto.DepartmentID) return BadRequest();

            try
            {
                var department = await _context.Departments.FindAsync(id);
                if (department == null) return NotFound();

                department.DepartmentName = dto.DepartmentName;
                department.Description = dto.Description;

                _context.Entry(department).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/department/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                var department = await _context.Departments.FindAsync(id);
                if (department == null) return NotFound();

                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
