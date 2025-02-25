namespace SpecRandomizer.Server.Model
{
    public class Player
    {
        public int PlayerId { get; set; }
        public int ConfigurationId { get; set; }
        public SpecList[] SpecList { get; set; }
        public string PlayerName { get; set; }
        public virtual Configuration Configuration { get; }

    }
}
