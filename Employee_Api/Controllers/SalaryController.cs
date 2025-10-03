using Employee_Api.Data;
using Employee_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        public SalaryController(EmployeeDbContext context)
        {
            _context = context;
        }

        // ✅ GET: api/salary
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Salary>>> GetSalaries()
        {
            var salaries = await _context.Salaries
                .Include(s => s.Employee) // optional: to include employee details
                .ToListAsync();

            return Ok(salaries);
        }

        // ✅ GET: api/salary/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Salary>> GetSalary(int id)
        {
            var salary = await _context.Salaries
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(s => s.SalaryID == id);

            if (salary == null)
            {
                return NotFound();
            }

            return Ok(salary);
        }

        // ✅ POST: api/salary
        [HttpPost]
        public async Task<ActionResult<Salary>> CreateSalary(Salary salary)
        {
            _context.Salaries.Add(salary);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSalary), new { id = salary.SalaryID }, salary);
        }

        // ✅ PUT: api/salary/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSalary(int id, Salary salaryUpdate)
        {
            if (id != salaryUpdate.SalaryID)
            {
                return BadRequest("Salary ID mismatch");
            }

            _context.Entry(salaryUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Salaries.Any(s => s.SalaryID == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // ✅ DELETE: api/salary/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalary(int id)
        {
            var salary = await _context.Salaries.FindAsync(id);
            if (salary == null)
            {
                return NotFound();
            }

            _context.Salaries.Remove(salary);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
