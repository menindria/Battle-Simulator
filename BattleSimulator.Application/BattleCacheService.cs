using System.Collections.Generic;
using System.Threading.Tasks;
using BattleSimulator.Application.Contracts.Commands;
using BattleSimulator.Application.Contracts.Responses;
using BattleSimulator.Application.Contracts.Services;
using BattleSimulator.CrossCutting;
using BattleSimulator.Domain;

namespace BattleSimulator.Application
{
    public class BattleCacheService : IBattleService
    {
        private readonly IBattleService _battleService;

        private static readonly object CacheLock = new object();
        private static Battle _cache;

        public BattleCacheService(
            IBattleService battleService)
        {
            _battleService = battleService;
        }

        public Task<IResponse> AddArmyToBattleAsync(AddArmyCommand command)
        {
            return _battleService.AddArmyToBattleAsync(command);
        }

        public async Task<Battle> GetBattleByIdAsync(int battleId)
        {
            Battle battle = await _battleService.GetBattleByIdAsync(battleId);
            
            lock (CacheLock)
            {
                return _cache ?? (_cache = battle);
            }
        }

        public Task<IResponse> CreateBattleAsync(CreateBattleCommand command)
        {
            return _battleService.CreateBattleAsync(command);
        }

        public Task<IEnumerable<BattleDto>> GetAllBattlesAsync()
        {
            return _battleService.GetAllBattlesAsync();
        }

        public async Task ResetBattleAsync(int battleId)
        {
            await _battleService.ResetBattleAsync(battleId);
            _cache = null;
        }
    }
}
