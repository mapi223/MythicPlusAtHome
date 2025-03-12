using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SpecRandomizer.Server.Model;
using SpecRandomizer.Server.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace SpecRandomizer.Server.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        private readonly SpecRandomizerDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthenticationController(IConfiguration configuration, SpecRandomizerDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }


        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDTO request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.UserName = request.UserName;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.UserId = 0;
            User newUser = user;
            bool userExists = await _context.Users.AnyAsync(u => u.UserName == user.UserName);
            if(userExists)
            {
                return BadRequest("User Name Already Exists");
            }


            _context.Users.Add(user);
            await _userManager.AddToRoleAsync(user, "User");
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDTO request)
        {
            bool userExists = await _context.Users.AnyAsync(u => u.UserName == request.UserName);
            if (!userExists)
                return BadRequest("User not found");
            user = await _context.Users.FirstAsync(u => u.UserName == request.UserName);
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                return BadRequest("Wrong password");

            string token = "Valid Login Yay";

            return Ok(new {token, user.UserId});
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] UserDTO model)
        {
            var user = await _context.Users.FindAsync(model.UserId);
                if (user == null) return NotFound("User not found");
            IdentityResult result;
            if (user.UserId <= 2)
            {
                 result = await _userManager.AddToRoleAsync(user, "Admin");
            }
            else
            {
                result = await _userManager.AddToRoleAsync(user, "User");
            }
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok(new { Message = "Role assigned successfully" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("change-role")]
        public async Task<IActionResult> ChangeRole([FromBody] UserDTO model)
        {
            var user = await _context.Users.FindAsync(model.UserId);
            if (user == null) return NotFound("User not found");

            var role = await _userManager.IsInRoleAsync(user, "Admin");
            IdentityResult result;
            if(role != true)
            {
                result = await _userManager.AddToRoleAsync(user, "Admin");
                if (!result.Succeeded) return BadRequest(result.Errors);
                return Ok(user);
            }
            

            return BadRequest(new { Message = "User is already an Admin" });
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, "Admin")
            };
            var tokenKey = _configuration.GetSection("AppSettings:Token").Value;
            if (string.IsNullOrEmpty(tokenKey))
            {
                throw new Exception("JWT Secret Key is missing from configuration");
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: cred
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }


}

