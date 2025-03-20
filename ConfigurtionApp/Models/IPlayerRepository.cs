using SpecRandomizer.Server.Model;

namespace ConfigurtionService.Models
{
    public interface IPlayerRepository
    {
        IEnumerable<ClassList> GetSpecList(int PlayerId);


    }
}
