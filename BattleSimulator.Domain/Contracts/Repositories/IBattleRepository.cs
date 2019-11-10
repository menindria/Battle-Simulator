using System.Collections.Generic;
using System.Threading.Tasks;

namespace BattleSimulator.Domain.Contracts.Repositories
{
    public interface IBattleRepository
    {
        Task<Battle> GetByBattleId(int id);
        Task Create(Battle battle);
        Task<IEnumerable<Battle>> GetAllBattles();
    }
}