using System;

namespace BattleSimulator.Domain
{
    public class Log : EntityBase
    {
        public Battle Battle { get; private set; }
        public int BattleId { get; private set; }
        public Army ArmyOne { get; private set; }
        public int ArmyOneId { get; private set; }
        public Army ArmyTwo { get; private set; }
        public int? ArmyTwoId { get; private set; }
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
                BattleId = battle.Id,
                LogType = LogType.Attack,
                ArmyOneId =  offensiveArmy.Id,
                ArmyTwoId = defensiveArmy.Id
            };
        }
        
        public static Log CreateReloadLog(Battle battle, Army army)
        {
            return new Log()
            {
                BattleId = battle.Id,
                LogType = LogType.Reload,
                ArmyOneId = army.Id
            };
        }

        public static Log CreateAttackedByLog(Battle battle, Army offensiveArmy, Army defensiveArmy)
        {
            return new Log()
            {
                BattleId = battle.Id,
                LogType = LogType.AttackedBy,
                ArmyOneId = defensiveArmy.Id,
                ArmyTwoId = offensiveArmy.Id
            };
        }
    }


}
