using System.Collections.Generic;
using System.Threading.Tasks;
using BattleSimulator.Application.Contracts.Responses;

namespace BattleSimulator.Application.Contracts.Services
{
    public interface IBattleLogService
    {
        Task<IReadOnlyCollection<LogDto>> GetLogs(int battleId);
    }
}