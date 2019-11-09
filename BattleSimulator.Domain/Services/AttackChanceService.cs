using System;
using BattleSimulator.Domain.Contracts.Services;

namespace BattleSimulator.Domain.Services
{
    public class AttackChanceService : IAttackChanceService
    {
        private static readonly Random Random = new Random();

        public bool IsSuccessful(Army army)
        {
            decimal probability = army.NumberOfUnits - army.NumberOfAttacks / 2m;

            return Random.Next(100) <= probability;
        }
    }
}
