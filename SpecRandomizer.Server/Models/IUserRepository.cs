namespace SpecRandomizer.Server.Models
{
    public interface IUserRepository
    {
        string getPassword(int userId);
    }
}
