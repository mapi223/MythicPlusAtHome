using Microsoft.AspNetCore.Mvc;
using ConfigurtionService.Models;



namespace ConfigurtionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly ConfigurationService _configurationService;


        public ConfigurationController(ConfigurationService configurationService)
        {
            _configurationService = configurationService;

        }

        [HttpPost]
        public async Task<IActionResult> PostConfiguration([FromBody] Configuration config)
        {
            var newConfig = await _configurationService.AddConfigurationAsync(config);
            return CreatedAtAction(nameof(GetConfigurationById), new { id = newConfig.ConfigurationId }, newConfig);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetConfigurationById(int id)
        {
            var config = await _configurationService.GetConfigurationByIdAsync(id);
            if (config == null)
            {
                return NotFound();
            }
            return Ok(config);
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<List<ConfigurationDto>>> GetAllConfigurationByUserId(int id)
        {
            return await _configurationService.GetAllConfigurationsByUserIdAsync(id);

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfiguration(int id)
        {
            var deleted = await _configurationService.DeleteConfigurationAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }


    }
}