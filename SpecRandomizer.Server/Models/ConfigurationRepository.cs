using SpecRandomizer.Server.Model;

namespace SpecRandomizer.Server.Models
{
    public class ConfigurationRepository: IConfigurationRepository
    {
        private readonly SpecRandomizerDbContext _specRandomizerDbContext;

        public ConfigurationRepository(SpecRandomizerDbContext? specRandomizerDbContext)
        {
            _specRandomizerDbContext = specRandomizerDbContext;
        }

        public IEnumerable<Configuration> AllConfigurations {
            get
            {
                return _specRandomizerDbContext.Configurations;
            }
           }
        public IEnumerable<Configuration>? getAllWithUserId(int UserId)
        {
            return _specRandomizerDbContext.Configurations.Where(u => u.UserId == UserId).ToList();
        }
        public Configuration? GetConfigurationWithIdAndUserID(int ConfigurationId, int UserId)
        {
            return _specRandomizerDbContext.Configurations.First(Configuration => Configuration.UserId == UserId && Configuration.ConfigurationId == ConfigurationId);
        }
    }
}

