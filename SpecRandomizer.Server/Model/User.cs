namespace SpecRandomizer.Server.Model
{
    public class User
    {
        private int id;
        private string userName;
        private string Password;

        public virtual ICollection<Configuration> Configurations{ get; set;}
    }
}
