using System;
using BattleSimulator.Domain;

namespace BattleSimulator.Application.Contracts.Responses
{
    public class LogDto
    {
        public int Id { get; set; }
        public int OffensiveArmy { get; set; }
        public int? DefensiveArmy { get; set; }
        public LogType LogType { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
