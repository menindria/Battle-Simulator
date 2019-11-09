using System;
using System.Collections.Generic;

namespace BattleSimulator.Domain
{
    public class Army : EntityBase
    {
        public int NumberOfAttacks { get; private set; }
        public string Name { get; private set; }
        public int NumberOfUnits { get; private set; } //TODO:JJ Can be used type with smaller size
        public StrategyAndAttackOptions StrategyAndAttackOption { get; private set; }
        public bool IsDead => NumberOfAttacks >= NumberOfUnits;
        public List<Log> Logs { get; set; } = new List<Log>();
        protected Army() { }

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

        public TimeSpan GetReloadTime()
        {
            return TimeSpan.FromSeconds(NumberOfUnits * 0.01);
        }
    }
}
