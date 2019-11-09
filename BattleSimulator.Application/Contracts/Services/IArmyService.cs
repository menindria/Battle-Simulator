using System.Threading.Tasks;
using BattleSimulator.Domain;

namespace BattleSimulator.Application.Contracts.Services
{
    public interface IArmyService
    {
        Task Attack(Battle battle, Army offensiveArmy, Army defensiveArmy);
        Task Reload(Battle battle, Army offensiveArmy);
        Task<Army> GetArmyByIdAsync(int id);
    }
}