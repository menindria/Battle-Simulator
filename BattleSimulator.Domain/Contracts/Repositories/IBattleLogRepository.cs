using System.Collections.Generic;
using System.Threading.Tasks;

namespace BattleSimulator.Domain.Contracts.Repositories
{
    public interface IBattleLogRepository
    {
        Task Add(Log log);
        Task<IReadOnlyCollection<Log>> GetLogs(int battleId);
        Task<Log> GetLastLogForArmy(int army);
    }
}