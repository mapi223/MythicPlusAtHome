using SpecRandomizer.Server.Model;

namespace SpecRandomizer.Server.Models
{
    public interface IPlayerRepository
    {
        IEnumerable<ClassList> GetSpecList(int PlayerId);


    }
}
