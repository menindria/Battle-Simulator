using System;
using System.Linq;
using System.Threading.Tasks;
using BattleSimulator.Domain;
using BattleSimulator.Domain.Contracts.Repositories;
using BattleSimulator.Domain.Contracts.Services;
using Microsoft.EntityFrameworkCore;

namespace BattleSimulator.Infrastructure.DataAccess.AttackStrategies
{
    public class RandomAttackStrategy : IAttackStrategy
    {
        private readonly IArmyRepository _armyRepository;
        private static readonly Random Random = new Random();

        public RandomAttackStrategy(IArmyRepository armyRepository)
        {
            _armyRepository = armyRepository;
        }

        public async Task<Army> ExecuteAsync(int id)
        {
            var allArmies = await _armyRepository
                .GetArmies()
                .Where(x => x.NumberOfUnits > x.NumberOfAttacks / 2)
                .Where(x => x.Id != id)
                .ToListAsync();

            if (allArmies.Any())
            {
                return allArmies.ElementAt(Random.Next(allArmies.Count));
            }

            return null;

        }

        public StrategyAndAttackOptions Option => StrategyAndAttackOptions.Random;
    }
}
