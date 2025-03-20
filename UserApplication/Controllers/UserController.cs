using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Packaging;
using UserApplication.Models;
using UserApplication.Services;



namespace UserApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SpecRandomizerDbContext _context;
        private readonly UserService _userService;

        public UserController(SpecRandomizerDbContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
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
            user.ModifiedBy = user;
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

    }

    
}