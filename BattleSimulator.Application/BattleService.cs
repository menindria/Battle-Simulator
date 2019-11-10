using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleSimulator.Application.Contracts.Commands;
using BattleSimulator.Application.Contracts.Responses;
using BattleSimulator.Application.Contracts.Services;
using BattleSimulator.CrossCutting;
using BattleSimulator.Domain;
using BattleSimulator.Domain.Contracts.Repositories;

namespace BattleSimulator.Application
{
    public class BattleService : IBattleService
    {
        private readonly IBattleRepository _battleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BattleService(
            IBattleRepository battleRepository,
            IUnitOfWork unitOfWork)
        {
            _battleRepository = battleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResponse> CreateBattleAsync(CreateBattleCommand command)
        {
            var battle = new Battle(command.Name);

            await _battleRepository.Create(battle);
            await _unitOfWork.SaveChanges();

            return new SuccessResponse();
        }

        public async Task<IResponse> AddArmyToBattleAsync(AddArmyCommand command)
        {
            Battle battle = await _battleRepository.GetByBattleId(command.BattleId);
            
            IResponse response = battle.AddArmy(new Army(command.Name, command.NumberOfUnits, command.StrategyAndAttackOption));

            if (!response.Success)
            {
                return response;
            }

            await _unitOfWork.SaveChanges();
            return response;
        }

        public Task<Battle> GetBattleByIdAsync(int battleId)
        {
            return _battleRepository.GetByBattleId(battleId);
        }
        
        public async Task<IEnumerable<BattleDto>> GetAllBattlesAsync()
        {
            return (await _battleRepository.GetAllBattles())
                .Select(x => new BattleDto()
                {
                    Id = x.Id,
                    Name = x.Name
                });
        }

        public async Task ResetBattleAsync(int battleId)
        {
            Battle battle = await _battleRepository.GetByBattleId(battleId);
            battle.Stop();
            battle.ClearLogs();
            await _unitOfWork.SaveChanges();
        }
    }
}
