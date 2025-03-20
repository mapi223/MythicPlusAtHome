

namespace GroupBuildingService.Models
{
    public interface IPlayerRepository
    {
        IEnumerable<ClassList> GetSpecList(int PlayerId);


    }
}
