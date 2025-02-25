using SpecRandomizer.Server.Model;

namespace SpecRandomizer.Server.Models
{
    public interface IPlayerRepository
    {
        IEnumerable<SpecList> GetSpecList(int PlayerId);


    }
}
