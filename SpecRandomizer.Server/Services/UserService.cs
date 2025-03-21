using Microsoft.EntityFrameworkCore;
using SpecRandomizer.Server.Models;

namespace SpecRandomizer.Server.Services
{
    public class UserService
    {

        private readonly SpecRandomizerDbContext _context;

        public UserService(SpecRandomizerDbContext context)
        {
            _context = context;
        }


        public async Task<bool> IsUserAdminAsync(int userId)
        {
            return await _context.UserRoles
                .AnyAsync(ur => ur.UserId == userId && ur.RoleId == 1);
        }
    }
}
