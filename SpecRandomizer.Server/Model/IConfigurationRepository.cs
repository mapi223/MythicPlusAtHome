namespace SpecRandomizer.Server.Model
{
    public interface IConfigurationRepository
    {
        IEnumerable<Configuration> AllConfigurations { get; }
    }
}
