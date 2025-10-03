using Employee_Api.Data;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        public LoginController(EmployeeDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => UserName == request.UserName
                                       && u.Password == request.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            // Return minimal info for frontend
            var response = new
            {
                userType = user.UserType,
                userName = user.UserName,
                schoolId = user.SchoolId,
                token = "dummy-token"  // later replace with JWT if needed
            };

            return Ok(response);
        }
    }
}
