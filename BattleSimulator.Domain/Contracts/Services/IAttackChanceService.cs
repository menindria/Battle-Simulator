namespace BattleSimulator.Domain.Contracts.Services
{
    public interface IAttackChanceService
    {
        bool IsSuccessful(Army army);
    }
}