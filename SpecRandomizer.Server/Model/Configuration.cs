namespace SpecRandomizer.Server.Model
{
    public class Configuration
    {
        private int id { get; set; }
        private int UserId { get; set; }
        private Player[] playerList { get; set; }
        public virtual User User { get; set; }
    }
}
