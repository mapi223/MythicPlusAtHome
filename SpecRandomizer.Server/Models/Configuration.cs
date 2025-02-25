namespace SpecRandomizer.Server.Model
{
    public class Configuration
    {
        public int ConfigurationId { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public virtual User User { get; set; }
    }
}
