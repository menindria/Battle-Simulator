using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BattleSimulator.Domain.Contracts.Services;

namespace BattleSimulator.Domain
{
    public class Army : EntityBase
    {
        private static readonly object LockNumberOfAttacks = new object();

        public Battle Battle { get; private set; }
        public int BattleId { get; private set; }

        private int? _numberOfAttacks;
        public int NumberOfAttacks
        {
            get
            {
                if (_numberOfAttacks == null)
                {
                    _numberOfAttacks = Logs.Count(x => x.LogType == LogType.AttackedBy);
                }
                return _numberOfAttacks.Value;
            }
        }

        public string Name { get; private set; }
        public int NumberOfUnits { get; private set; } //TODO:JJ Can be used type with smaller size
        public StrategyAndAttackOptions StrategyAndAttackOption { get; private set; }
        public bool IsDead => NumberOfAttacks / 2 >= NumberOfUnits;
        public List<Log> Logs { get; private set; } = new List<Log>();
        protected Army() { }

        private Log LastLog { get; set; }

        public Army(string name, int numberOfUnits, StrategyAndAttackOptions strategyAndAttackOption)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentOutOfRangeException(nameof(name));
            }

            if (numberOfUnits < 80 || numberOfUnits > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfUnits));
            }


            Name = name;
            NumberOfUnits = numberOfUnits;
            StrategyAndAttackOption = strategyAndAttackOption;
        }

        public async Task NexAction(
            Army defensiveArmy, 
            IAttackChanceService attackChanceService,
            Func<Log, Task> logger)
        {
            if (IsDead)
            {
                return;
            }

            if (LastLog == null || LastLog.LogType == LogType.Reload)
            {
                await Attack(defensiveArmy, attackChanceService, logger);
            }
            else
            {
                await Reload(logger);
            }
        }

        private async Task AttackedBy(Army offensiveArmy, Func<Log, Task> logger)
        {
            lock (LockNumberOfAttacks)
            {
                _numberOfAttacks++;
            }
            
            await LogAction(Log.CreateAttackedByLog(Battle, offensiveArmy, this), logger);
            Debug.WriteLine($".................{offensiveArmy.Name} Attacked by {Name}.......................");
        }

        private async Task Attack(Army defensiveArmy, IAttackChanceService attackChanceService, Func<Log, Task> logger)
        {
            if (attackChanceService.IsSuccessful(this))
            {
                await defensiveArmy.AttackedBy(this, logger);
                await LogAction(Log.CreateAttackLog(Battle, this, defensiveArmy), logger);
            }
        }

        private async Task Reload(Func<Log, Task> logger)
        {
            await Task.Delay(GetReloadTime());
            await LogAction(Log.CreateReloadLog(Battle, this), logger);
        }

        private TimeSpan GetReloadTime()
        {
            return TimeSpan.FromSeconds(NumberOfUnits * 0.01);
        }

        private async Task LogAction(Log log, Func<Log, Task> logger) //TODO:JJ Logging code have code smell, it should be refactored
        {
            await logger(log);
            LastLog = log;
        }
    }
}
