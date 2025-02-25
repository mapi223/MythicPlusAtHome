namespace SpecRandomizer.Server.Model
{
    public interface IConfigurationRepository
    {
        IEnumerable<Configuration> AllConfigurations { get; }
        IEnumerable<Configuration>? getAllWithUserId(int userId);
        Configuration? GetConfigurationWithIdAndUserID(int configurationId, int userId);
    }
}
