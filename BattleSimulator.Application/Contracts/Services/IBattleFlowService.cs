using System.Threading.Tasks;
using BattleSimulator.Application.Contracts.Responses;
using BattleSimulator.CrossCutting;

namespace BattleSimulator.Application.Contracts.Services
{
    public interface IBattleFlowService
    {
        Task<IResponse> Start(int battleId);
        Task<IResponse> ResetAsync(int battleId);
    }
}