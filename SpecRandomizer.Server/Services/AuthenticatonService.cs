using Microsoft.IdentityModel.Tokens;
using SpecRandomizer.Server.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace SpecRandomizer.Server.Services
{
    public class AuthenticatonService
    {
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

        private string CreateToken(User user, IConfiguration _configuration)
        {
            List<Claim> claims = new List<Claim>
                {
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim(ClaimTypes.Role,"Admin")
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));
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

