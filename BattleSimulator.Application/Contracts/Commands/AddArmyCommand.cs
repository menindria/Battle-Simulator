using BattleSimulator.Domain;

namespace BattleSimulator.Application.Contracts.Commands
{
    public class AddArmyCommand
    {
        public int BattleId { get; set; }
        public string Name { get; set; }
        public int NumberOfUnits { get; set; } //TODO:JJ Can be used type with smaller size
        public StrategyAndAttackOptions StrategyAndAttackOption { get; set; }
    }
}
