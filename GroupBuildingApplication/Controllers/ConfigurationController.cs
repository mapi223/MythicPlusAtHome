using GroupBuildingService.Models;
using GroupBuildingService.Services;
using Microsoft.AspNetCore.Mvc;


namespace GroupBuildingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly GroupConfigurationService _groupConfigurationService;
        private readonly ConfigurationService _configurationService;

        public ConfigurationController(ConfigurationService configurationService, GroupConfigurationService groupConfigurationService)
        {
            _configurationService = configurationService;
            _groupConfigurationService = groupConfigurationService;
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