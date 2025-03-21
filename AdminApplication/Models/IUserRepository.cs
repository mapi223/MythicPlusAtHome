namespace AdminApplication.Models
{
    public interface IUserRepository
    {
        string getPassword(int UserId);
    }
}
