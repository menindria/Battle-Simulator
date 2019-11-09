using System.Threading.Tasks;
using BattleSimulator.CrossCutting;
using BattleSimulator.Domain;

namespace BattleSimulator.Infrastructure.Simulator.Simulator.Contracts
{
    public interface IBattleSimulatorService
    {
        Task<IResponse> SimulateAsync(Battle battle);
    }
}