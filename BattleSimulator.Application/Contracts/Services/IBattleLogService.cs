using System.Collections.Generic;
using System.Threading.Tasks;
using BattleSimulator.Application.Contracts.Responses;
using BattleSimulator.Domain;

namespace BattleSimulator.Application.Contracts.Services
{
    public interface IBattleLogService
    {
        Task<IReadOnlyCollection<LogDto>> GetLogs(int battleId);
        Task<Log> GetLastLogForArmy(int armyId);
    }
}