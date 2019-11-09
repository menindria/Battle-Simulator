using System.Threading.Tasks;

namespace BattleSimulator.Domain.Contracts.Services
{
    public interface IAttackStrategy
    {
        Task<Army> ExecuteAsync(int id);
        StrategyAndAttackOptions Option { get; }
    }
}