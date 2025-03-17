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
    }
}
