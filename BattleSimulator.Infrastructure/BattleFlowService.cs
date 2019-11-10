using System.Threading.Tasks;
using BattleSimulator.Application.Contracts.Services;
using BattleSimulator.CrossCutting;
using BattleSimulator.Domain;
using BattleSimulator.Infrastructure.Simulator.Simulator.Contracts;

namespace BattleSimulator.Infrastructure.Simulator
{
    public class BattleFlowService : IBattleFlowService
    {
        private readonly IBattleService _battleService;
        private readonly IBattleSimulatorService _battleSimulatorService;
        private readonly IUnitOfWork _unitOfWork;

        public BattleFlowService(
            IBattleService battleService,
            IBattleSimulatorService battleSimulatorService,
            IUnitOfWork unitOfWork)
        {
            _battleService = battleService;
            _battleSimulatorService = battleSimulatorService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResponse> Start(int battleId)
        {

            Battle battle = await _battleService.GetBattleByIdAsync(battleId);

            if (battle == null){ return new ErrorResponse("Battle not found"); }

            await ResetAsync(battleId);

            IResponse response = battle.Start();

            if (!response.Success)
            {
                return response;
            }

            await _battleSimulatorService.SimulateAsync(battle);
            
            await _unitOfWork.SaveChanges();

            return new SuccessResponse();
        }

        public async Task<IResponse> ResetAsync(int battleId)
        {
            //TODO:JJ Here we need to stop simulation
            //I didn't find any easy solution to stop job in reasonable time
            await _battleService.ResetBattleAsync(battleId);
            return await Task.FromResult(new SuccessResponse());
        }
    }
}
