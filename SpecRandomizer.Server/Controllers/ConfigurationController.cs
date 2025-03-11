using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpecRandomizer.Server.Model;
using SpecRandomizer.Server.Models;
using SpecRandomizer.Server.Services;
using System.Configuration;


namespace SpecRandomizer.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly ConfigurationService _configurationService;
        private readonly GroupConfigurationService _groupConfigurationService;

        public ConfigurationController(ConfigurationService configurationService, GroupConfigurationService groupConfigurationService)
        {
            _configurationService = configurationService;
            _groupConfigurationService = groupConfigurationService;
        }

        [HttpPost]
        public async Task<IActionResult> PostConfiguration([FromBody] Model.Configuration config)
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
        public async Task<ActionResult<List<Model.ConfigurationDto>>> GetAllConfigurationByUserId(int id)
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

        [HttpGet("/group/")]
        public List<RoleAssignmentDto> GetGroupLayout()
        {
            var config = _configurationService.GetConfigurationByNewestAsync().Result;
            if (config == null)
            {
                List<RoleAssignmentDto> emtpyList = new();
                return emtpyList;
            }
            return _groupConfigurationService.GetRoleAssignments(config);

        }

    }
}