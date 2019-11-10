using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BattleSimulator.Application.Contracts.Services;
using BattleSimulator.CrossCutting;
using BattleSimulator.Domain;
using BattleSimulator.Domain.Contracts.Repositories;
using BattleSimulator.Domain.Contracts.Services;
using BattleSimulator.Infrastructure.Simulator.Simulator.Contracts;
using Hangfire;

namespace BattleSimulator.Infrastructure.Simulator.Simulator
{
    public class BattleSimulatorService : IBattleSimulatorService
    {
        private readonly IAttackStrategyComposite _attackStrategyComposite;
        private readonly IBattleService _battleService;
        private readonly IAttackChanceService _attackChanceService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBattleLogRepository _battleLogRepository;

        public BattleSimulatorService(
            IAttackStrategyComposite attackStrategyComposite,
            IBattleService battleService,
            IAttackChanceService attackChanceService,
            IUnitOfWork unitOfWork,
            IBattleLogRepository battleLogRepository)
        {
            _attackStrategyComposite = attackStrategyComposite;
            _battleService = battleService;
            _attackChanceService = attackChanceService;
            _unitOfWork = unitOfWork;
            _battleLogRepository = battleLogRepository;
        }

        public Task<IResponse> SimulateAsync(Battle battle)
        {
            BackgroundJob.Enqueue(() => Start(battle.Id, JobCancellationToken.Null));

            return Task.FromResult<IResponse>(new SuccessResponse());
        }

        public async Task Start(int battleId, IJobCancellationToken cancellationToken)
        {
            Battle battle = await _battleService.GetBattleByIdAsync(battleId);

            var workers = new List<Task>();
            foreach (Army army in battle.Armies.Where(x => !x.IsDead))
            {
                workers.Add(Task.Run(() => Execute(army, cancellationToken)));
            }

            await Task.WhenAll(workers);
            battle.Stop();
            await _unitOfWork.SaveChanges();
        }

        private async Task Execute(Army offensiveArmy, IJobCancellationToken cancellationToken)
        {
            while (true)
            {
                if (cancellationToken.ShutdownToken.IsCancellationRequested)
                {
                    return;
                }

                if (offensiveArmy.IsDead)
                {
                    Debug.WriteLine($".................{offensiveArmy.Id} Killed.......................");
                    return;
                }

                Army defensiveArmyToAttack = await _attackStrategyComposite.ExecuteAsync(offensiveArmy);

                if (defensiveArmyToAttack == null)
                {
                    Debug.WriteLine($".................{offensiveArmy.Id} Winner.......................");
                    return;
                }

                await offensiveArmy.NexAction(defensiveArmyToAttack, _attackChanceService, _battleLogRepository.AddLogThreadSafe);
            }
        }
    }
}
