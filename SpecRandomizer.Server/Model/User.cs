namespace SpecRandomizer.Server.Model
{
    public class User
    {
        private int userId { get; set; }
        private string userName { get; set; }
        private string Password { get; set; }

        public virtual ICollection<Configuration> Configurations{ get; set;}
    }
}
