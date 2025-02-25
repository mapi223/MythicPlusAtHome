namespace SpecRandomizer.Server.Model
{
    public interface IConfigurationRepository
    {
        IEnumerable<Configuration> AllConfigurations { get; }
        IEnumerable<Configuration>? getAllWithUserId(int UserId);
        Configuration? GetConfigurationWithIdAndUserID(int ConfigurationId, int UserId);
    }
}
