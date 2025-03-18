using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpecRandomizer.Server.Model;
using SpecRandomizer.Server.Models;
using SpecRandomizer.Server.Services;
using System.Configuration;
using System.Threading.Tasks;
using Configuration = SpecRandomizer.Server.Model.Configuration;


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
        [HttpGet("admin/{userId}")]
        public async Task<List<ConfigurationDto>> getConfigurationDtosForAdminSpecificUser(int userId, [FromQuery] int AdminId)
        {
            bool isAdmin = await _configurationService.IsUserAdminAsync(AdminId);
            if (!isAdmin)
            {
                throw new UnauthorizedAccessException("Only Admins can get all configurations for another user");
            }

            return await _configurationService.GetAllConfigurationsByUserIdAsync(userId);

        }

        [HttpPut("{UserId}")]
        public async Task<ConfigurationDto> updateConfigurationForAdminSpecificUserSpecificConfiguration([FromRoute] int UserId, [FromQuery] int modifierId, [FromBody] Configuration Config)
        {
            bool isAdmin = await _configurationService.IsUserAdminAsync(modifierId);
            if (!isAdmin)
            {
                throw new UnauthorizedAccessException("Only admins may update configurations for Users");
            }

            var result = await _configurationService.UpdateConfigurationAsync(Config, modifierId, UserId);
            return ConfigurationDto.ConvertToDto(result);
        }

    }
}