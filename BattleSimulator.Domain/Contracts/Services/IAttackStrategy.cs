using System.Threading.Tasks;

namespace BattleSimulator.Domain.Contracts.Services
{
    public interface IAttackStrategy
    {
        Task<Army> ExecuteAsync(Army offensiveArmy);
        StrategyAndAttackOptions Option { get; }
    }
}