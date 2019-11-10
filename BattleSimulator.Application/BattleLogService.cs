using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleSimulator.Application.Contracts.Responses;
using BattleSimulator.Application.Contracts.Services;
using BattleSimulator.Domain.Contracts.Repositories;

namespace BattleSimulator.Application
{
    public class BattleLogService : IBattleLogService
    {
        private readonly IBattleLogRepository _battleLogRepository;

        public BattleLogService(IBattleLogRepository battleLogRepository)
        {
            _battleLogRepository = battleLogRepository;
        }

        public async Task<IReadOnlyCollection<LogDto>> GetLogs(int battleId)
        {
            ///TODO: This mapping can be in upper layers, and also can be done with automapper
            return (await _battleLogRepository.GetLogs(battleId))
                .Select(x => new LogDto()
                {
                    Id = x.Id,
                    LogType = x.LogType,
                    OffensiveArmy = x.ArmyOneId,
                    DefensiveArmy = x.ArmyTwoId,
                    TimeStamp = x.TimeStamp,
                }).ToList();
        }
    }
}
