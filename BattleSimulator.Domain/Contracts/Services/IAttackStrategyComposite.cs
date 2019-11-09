using System.Threading.Tasks;

namespace BattleSimulator.Domain.Contracts.Services
{
    public interface IAttackStrategyComposite
    {
        Task<Army> ExecuteAsync(int id, StrategyAndAttackOptions strategy);
    }
}