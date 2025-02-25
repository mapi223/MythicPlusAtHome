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

        IEnumerable<Configuration> AllConfigurations {
            get
            {
                return _specRandomizerDbContext.Configurations;
            }
           }
        IEnumerable<Configuration>? getAllWithUserId(int UserId)
        {
            return _specRandomizerDbContext.Configurations.f;
        }
        Configuration? GetConfigurationWithIdAndUserID(int ConfigurationId, int UserId);
    }
}
}
