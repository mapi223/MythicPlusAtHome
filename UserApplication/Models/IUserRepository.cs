namespace UserApplication.Models
{
    public interface IUserRepository
    {
        string getPassword(int UserId);
    }
}
