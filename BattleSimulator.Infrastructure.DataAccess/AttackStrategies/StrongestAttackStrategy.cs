using System.Linq;
using System.Threading.Tasks;
using BattleSimulator.Domain;
using BattleSimulator.Domain.Contracts.Repositories;
using BattleSimulator.Domain.Contracts.Services;
using Microsoft.EntityFrameworkCore;

namespace BattleSimulator.Infrastructure.DataAccess.AttackStrategies
{
    public class StrongestAttackStrategy : IAttackStrategy
    {
        private readonly IArmyRepository _armyRepository;

        public StrongestAttackStrategy(IArmyRepository armyRepository)
        {
            _armyRepository = armyRepository;
        }

        public Task<Army> ExecuteAsync(int id)
        {
            return _armyRepository
                .GetArmies()
                .Where(x => x.NumberOfUnits > x.NumberOfAttacks / 2)
                .OrderByDescending(x => x.NumberOfUnits - x.NumberOfAttacks)
                .FirstOrDefaultAsync(x => x.Id != id); 
        }

        public StrategyAndAttackOptions Option => StrategyAndAttackOptions.Strongest;
    }
}