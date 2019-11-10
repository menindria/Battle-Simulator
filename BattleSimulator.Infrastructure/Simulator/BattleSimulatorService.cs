using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using BattleSimulator.Application.Contracts.Services;
using BattleSimulator.CrossCutting;
using BattleSimulator.Domain;
using BattleSimulator.Domain.Contracts.Services;
using BattleSimulator.Infrastructure.Simulator.Simulator.Contracts;
using Hangfire;

namespace BattleSimulator.Infrastructure.Simulator.Simulator
{
    public class BattleSimulatorService : IBattleSimulatorService
    {
        private readonly IArmyService _armyService;
        private readonly IAttackStrategyComposite _attackStrategyComposite;
        private readonly IBattleLogService _battleLogService;
        private readonly IBattleService _battleService;
        private readonly IAttackChanceService _attackChanceService;

        public BattleSimulatorService(
            IArmyService armyService,
            IAttackStrategyComposite attackStrategyComposite,
            IBattleLogService battleLogService,
            IBattleService battleService,
            IAttackChanceService attackChanceService)
        {
            _armyService = armyService;
            _attackStrategyComposite = attackStrategyComposite;
            _battleLogService = battleLogService;
            _battleService = battleService;
            _attackChanceService = attackChanceService;
        }

        public Task<IResponse> SimulateAsync(Battle battle)
        {
            foreach (Army currentArmy in battle.Armies)
            {
                BackgroundJob.Enqueue(() => Start(battle.Id, currentArmy.Id, JobCancellationToken.Null));
            }
            return Task.FromResult<IResponse>(new SuccessResponse());
        }

        public async Task Start(int battleId, int offensiveArmyId, IJobCancellationToken cancellationToken)
        {
            Battle battle = await _battleService.GetBattleByIdAsync(battleId);
            LogType lastAction = (await _battleLogService.GetLastLogForArmy(offensiveArmyId))?.LogType ?? LogType.Reload;

            while (true)
            {
                if (cancellationToken.ShutdownToken.IsCancellationRequested)
                {
                    return;
                }

                Army offensiveArmy = await _armyService.GetArmyByIdAsync(offensiveArmyId); ///TODO:JJ To many calls to Database

                if (offensiveArmy.IsDead)
                {
                    Debug.WriteLine($".................{offensiveArmyId} Killed.......................");
                    return;
                }

                Army defensiveArmyToAttack = await _attackStrategyComposite.ExecuteAsync(offensiveArmy.Id, offensiveArmy.StrategyAndAttackOption);

                if (defensiveArmyToAttack == null)
                {
                    Debug.WriteLine($".................{offensiveArmyId} Winner.......................");
                    return;
                }

                if (lastAction == LogType.Reload && _attackChanceService.IsSuccessful(offensiveArmy)) ///TODO:JJ Code smell for LogType.Reload, should be refactored
                {
                    await _armyService.Attack(battle, offensiveArmy, defensiveArmyToAttack);
                }

                await Task.Delay(offensiveArmy.GetReloadTime());

                await _armyService.Reload(battle, offensiveArmy);
                lastAction = LogType.Reload; ///TODO:JJ Code smell, should be refactored
            }
        }
    }
}
