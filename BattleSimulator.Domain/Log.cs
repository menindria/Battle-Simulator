using System;

namespace BattleSimulator.Domain
{
    public class Log : EntityBase
    {
        public Battle Battle { get; private set; }
        public Army OffensiveArmy { get; private set; }
        public int OffensiveArmyId { get; private set; }
        public Army DefensiveArmy { get; private set; }
        public int? DefensiveArmyId { get; private set; }
        public LogType LogType { get; private set; }
        public DateTime TimeStamp { get; protected set; }
        protected Log()
        {
            TimeStamp = DateTime.UtcNow;
        }

        public static Log CreateAttackLog(Battle battle, Army offensiveArmy, Army defensiveArmy)
        {
            return new Log()
            {
                Battle = battle,
                LogType = LogType.Attack,
                OffensiveArmy = offensiveArmy,
                DefensiveArmy = defensiveArmy
            };
        }
        
        public static Log CreateReloadLog(Battle battle, Army offensiveArmy)
        {
            return new Log()
            {
                Battle = battle,
                LogType = LogType.Reload,
                OffensiveArmy = offensiveArmy,
            };
        }
    }
}
