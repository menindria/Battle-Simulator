using BattleSimulator.Domain;
using BattleSimulator.Domain.Contracts.Services;
using BattleSimulator.Domain.Services;
using BattleSimulator.Infrastructure.DataAccess.AttackStrategies;
using Microsoft.Extensions.DependencyInjection;

namespace BattleSimulator.Api.Modules
{
    public static class DomainModule
    {
        public static void RegisterDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IAttackStrategy, RandomAttackStrategy>();
            services.AddScoped<IAttackStrategy, StrongestAttackStrategy>();
            services.AddScoped<IAttackStrategy, WeakestAttackStrategy>();
            services.AddScoped<IAttackStrategyComposite, AttackStrategyComposite>();

            services.AddScoped<IAttackChanceService, AttackChanceService>();
        }
    }
}