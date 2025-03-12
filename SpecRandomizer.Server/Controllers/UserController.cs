using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpecRandomizer.Server.Model;
using SpecRandomizer.Server.Models;


namespace SpecRandomizer.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SpecRandomizerDbContext _context;

        public UserController(SpecRandomizerDbContext context)
        {
            _context = context;
        }

       
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid user data.");
            }
            bool userExists = await _context.Users.AnyAsync(u => u.UserName == user.UserName);
            if (userExists)
            {
                return BadRequest("User Already Exists");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.Configurations.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDTO userDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            user.UserName = userDto.UserName;

            await _context.SaveChangesAsync();
            return Ok(user);
        }


    }
}