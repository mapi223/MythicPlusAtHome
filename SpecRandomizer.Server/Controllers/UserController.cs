using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
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

        [HttpPut("{id}")]
        public async Task<User> UpdateUser([FromBody] User updatedUser, [FromBody] User Modifier, int id)
        {
            bool isAdmin = await IsUserAdminAsync(Modifier.UserId);

            if (!isAdmin)
            {
                throw new UnauthorizedAccessException($"Only Admins can update Users");
            }

           var existingUser = await _context.Users
         .Include(u => u.Configurations)
         .FirstOrDefaultAsync(u => u.UserId == id);

            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            existingUser.UserName = updatedUser.UserName;
            existingUser.PasswordHash = updatedUser.PasswordHash;
            existingUser.PasswordSalt = updatedUser.PasswordSalt;
            existingUser.ModifiedBy = Modifier;

            if (updatedUser.Configurations != null)
            {
                existingUser.Configurations.Clear();
                existingUser.Configurations.AddRange(updatedUser.Configurations);
            }

            return existingUser;
        }
        public async Task<bool> IsUserAdminAsync(int userId)
        {
            return await _context.UserRoles
                .AnyAsync(ur => ur.UserId == userId && ur.RoleId == 1);
        }

    }
}