using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Packaging;
using SpecRandomizer.Server.Model;
using SpecRandomizer.Server.Models;
using SpecRandomizer.Server.Services;


namespace SpecRandomizer.Server.Controllers
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

        [HttpPut("update/{id}")]
        public async Task<User> UpdateUser([FromRoute] int id, [FromQuery] int modifierId, [FromBody] UserDTO updatedUser)
        {
            User Modifier = await _context.Users.FirstOrDefaultAsync(u => u.UserId == modifierId);
            bool isAdmin = await _userService.IsUserAdminAsync(Modifier.UserId);

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
            existingUser.ModifiedBy = Modifier;
            existingUser.ModifiedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingUser;
        }


        [HttpGet("admin/{AdminId}")]
        public async Task<List<UserDTO>> GetAllUsersForAdmin(int AdminId)
        {
            User Modifier = await _context.Users.FirstOrDefaultAsync(u => u.UserId == AdminId);
            bool isAdmin = await _userService.IsUserAdminAsync(Modifier.UserId);

            if (!isAdmin)
            {
                throw new UnauthorizedAccessException($"Only Admins can get all Users");
            }

            List<UserDTO> users = _context.Users
                  .Select(u => new UserDTO
                  {
                      uId = u.UserId,
                      UserName = u.UserName,
                      Password = u.UserName
                  }).ToList();

            return users;

        }
    }

    
}