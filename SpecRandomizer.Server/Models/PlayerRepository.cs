using SpecRandomizer.Server.Model;

namespace SpecRandomizer.Server.Models
{
    public class PlayerRepository: IPlayerRepository
    {
        private readonly SpecRandomizerDbContext _specRandomizerDbContext;

        public PlayerRepository(SpecRandomizerDbContext specRandomizerDbContext)
        {
            _specRandomizerDbContext = specRandomizerDbContext;
        }

        public IEnumerable<ClassList> GetSpecList(int playerId)
        {
            Player player = _specRandomizerDbContext.Players.FirstOrDefault(p => p.PlayerId == playerId);
            if (player != null)
            {
                return player.SpecList;
            }
            else
                return [ClassList.NONE];
        }
    }
}
