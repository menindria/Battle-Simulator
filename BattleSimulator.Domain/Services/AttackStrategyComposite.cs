using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleSimulator.Domain.Contracts.Services;

namespace BattleSimulator.Domain.Services
{
    public class AttackStrategyComposite : IAttackStrategyComposite
    {
        private readonly IEnumerable<IAttackStrategy> _attackStrategies;

        public AttackStrategyComposite(
            IEnumerable<IAttackStrategy> attackStrategies)
        {
            _attackStrategies = attackStrategies;
        }

        public Task<Army> ExecuteAsync(int id, StrategyAndAttackOptions strategy)
        {
            return _attackStrategies.First(x => x.Option == strategy).ExecuteAsync(id);
        }
    }
}
