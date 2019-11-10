using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleSimulator.Application.Contracts.Services;
using BattleSimulator.Domain;
using BattleSimulator.Domain.Contracts.Services;

namespace BattleSimulator.Infrastructure.DataAccess.AttackStrategies
{
    public class RandomAttackStrategy : IAttackStrategy
    {
        private readonly IBattleService _battleService;
        private static readonly Random Random = new Random();

        public RandomAttackStrategy(IBattleService battleService)
        {
            _battleService = battleService;
        }

        public async Task<Army> ExecuteAsync(Army offensiveArmy)
        {
            Battle battle = await _battleService.GetBattleByIdAsync(offensiveArmy.BattleId);

            IEnumerable<Army> defensiveArmies =
                battle.Armies
                    .Where(x => !x.IsDead && x.Id != offensiveArmy.Id)
                    .ToList();

            if (defensiveArmies.Any())
            {
                return defensiveArmies.ElementAt(Random.Next(defensiveArmies.Count()));
            }

            return null;

        }

        public StrategyAndAttackOptions Option => StrategyAndAttackOptions.Random;
    }
}
