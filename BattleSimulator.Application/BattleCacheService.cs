using System.Collections.Generic;
using System.Threading.Tasks;
using BattleSimulator.Application.Contracts.Commands;
using BattleSimulator.Application.Contracts.Responses;
using BattleSimulator.Application.Contracts.Services;
using BattleSimulator.CrossCutting;
using BattleSimulator.Domain;
using Microsoft.Extensions.Caching.Memory;

namespace BattleSimulator.Application
{
    public class BattleCacheService : IBattleService
    {
        private readonly IBattleService _battleService;
        private readonly IMemoryCache _memoryCache;
        private const string Key = "ID";

        private Battle cache;

        public BattleCacheService(
            IBattleService battleService,
            IMemoryCache memoryCache)
        {
            _battleService = battleService;
            _memoryCache = memoryCache;
        }

        public Task<IResponse> AddArmyToBattleAsync(AddArmyCommand command)
        {
            return _battleService.AddArmyToBattleAsync(command);
        }

        public async Task<Battle> GetBattleByIdAsync(int battleId)
        {
            if (cache == null)
            {
                cache = await _battleService.GetBattleByIdAsync(battleId);
            }

            return cache;
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
            cache = null;
        }
    }
}
