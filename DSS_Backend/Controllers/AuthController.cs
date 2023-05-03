using DSS_Backend.Helpers;
using DSS_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DSS_Backend.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;

        public AuthController(DataContext context)
        {
            _context = context;
        }

        // POST: /auth/signin
        [HttpPost("signin")]
        public async Task<ActionResult<User>> Authenticate(User user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.name == user.name);
            if (dbUser == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            if (dbUser.password != user.password)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            return dbUser;
        }

        // POST: /auth/signup
        [HttpPost("signup")]
        public async Task<ActionResult<User>> Register(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUserById), new { id = user.id }, user);
        }

        [HttpPost("refreshtoken")]
        public IActionResult RefreshToken()
        {
            // refresh token logic

            return Ok();
        }

        // GET: /auth/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }
    }
}
