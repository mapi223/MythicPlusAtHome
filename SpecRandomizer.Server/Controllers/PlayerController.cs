using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpecRandomizer.Server.Model;
using SpecRandomizer.Server.Models;

namespace SpecRandomizer.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : Controller
    {

        private readonly SpecRandomizerDbContext _context;

        public PlayerController(SpecRandomizerDbContext context)
        {
            _context = context;
        }

        // POST: api/Configuration
        [HttpPost]
        public async Task<IActionResult> PostPlayer([FromBody] Player player)
        {
            if (player == null)
            {
                return BadRequest("Invalid configuration data.");
            }

            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPlayerById), new { id = player.PlayerId }, player);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayerById(int id)
        {
            var config = await _context.Players.FindAsync(id);
            if (config == null)
            {
                return NotFound();
            }
            return Ok(config);
        }
    }
}
