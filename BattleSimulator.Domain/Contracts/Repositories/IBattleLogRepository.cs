using System.Collections.Generic;
using System.Threading.Tasks;

namespace BattleSimulator.Domain.Contracts.Repositories
{
    public interface IBattleLogRepository
    {
        Task<IReadOnlyCollection<Log>> GetLogs(int battleId);
        Task AddLogThreadSafe(Log log);
    }
}