using System.Collections.Generic;
using System.Threading.Tasks;
using BattleSimulator.Application.Contracts.Commands;
using BattleSimulator.Application.Contracts.Responses;
using BattleSimulator.CrossCutting;
using BattleSimulator.Domain;

namespace BattleSimulator.Application.Contracts.Services
{
    public interface IBattleService
    {
        Task<IResponse> AddArmyToBattleAsync(AddArmyCommand command);
        Task<Battle> GetBattleByIdAsync(int battleId);
        Task<IResponse> CreateBattleAsync(CreateBattleCommand command);
        Task<IEnumerable<BattleDto>> GetAllBattlesAsync();
        Task ResetBattleAsync(int battleId);
    }
}