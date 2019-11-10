using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleSimulator.Application.Contracts.Services;
using BattleSimulator.Domain;
using BattleSimulator.Domain.Contracts.Services;

namespace BattleSimulator.Infrastructure.DataAccess.AttackStrategies
{
    public class WeakestAttackStrategy : IAttackStrategy
    {
        private readonly IBattleService _battleService;

        public WeakestAttackStrategy(IBattleService battleService)
        {
            _battleService = battleService;
        }

        public async Task<Army> ExecuteAsync(Army offensiveArmy)
        {
            Battle battle = await _battleService.GetBattleByIdAsync(offensiveArmy.BattleId);

            return
                battle.Armies
                    .Where(x => !x.IsDead && x.Id != offensiveArmy.Id)
                    .OrderBy(x => x.NumberOfUnits - x.NumberOfAttacks)
                    .FirstOrDefault();
        }

        public StrategyAndAttackOptions Option => StrategyAndAttackOptions.Weakest;
    }
}