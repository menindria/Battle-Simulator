using System.Diagnostics;
using System.Threading.Tasks;
using BattleSimulator.Application.Contracts.Services;
using BattleSimulator.Domain;
using BattleSimulator.Domain.Contracts.Repositories;

namespace BattleSimulator.Application
{
    public class ArmyService : IArmyService
    {
        private readonly IArmyRepository _armyRepository;
        private readonly IBattleLogRepository _battleLogRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ArmyService(
            IArmyRepository armyRepository,
            IBattleLogRepository battleLogRepository,
            IUnitOfWork unitOfWork)
        {
            _armyRepository = armyRepository;
            _battleLogRepository = battleLogRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Attack(Battle battle, Army offensiveArmy, Army defensiveArmy)
        {
            await _armyRepository.AttackArmy(defensiveArmy.Id);//Sql will prevent lost update
            await _battleLogRepository.Add(Log.CreateAttackLog(battle, offensiveArmy, defensiveArmy));
            await _unitOfWork.SaveChanges();
            Debug.WriteLine($".................{offensiveArmy.Name} Attacked {defensiveArmy.Name}.......................");
        }
        
        public async Task Reload(Battle battle, Army offensiveArmy)
        {
            await _battleLogRepository.Add(Log.CreateReloadLog(battle, offensiveArmy));
            await _unitOfWork.SaveChanges();
            Debug.WriteLine($".................{offensiveArmy.Name} Reloaded..............................");
        }

        public Task<Army> GetArmyByIdAsync(int id)
        {
            return _armyRepository.GetArmyByIdAsync(id);
        }
    }
}
