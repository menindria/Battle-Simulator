using System.Linq;
using System.Threading.Tasks;

namespace BattleSimulator.Domain.Contracts.Repositories
{
    public interface IArmyRepository
    {
        IQueryable<Army> GetArmies();
        Task AttackArmy(int id);
        Task<Army> GetArmyByIdAsync(int id);
    }
}