using BattleSimulator.Application;
using BattleSimulator.Application.Contracts.Services;
using BattleSimulator.Infrastructure.DataAccess.Repositories;
using BattleSimulator.Infrastructure.Simulator;
using BattleSimulator.Infrastructure.Simulator.Simulator;
using BattleSimulator.Infrastructure.Simulator.Simulator.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace BattleSimulator.Api.Modules
{
    public static class ApplicationModule
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IBattleFlowService, BattleFlowService>();
            services.AddScoped<IBattleLogService, BattleLogService>();

            services.AddScoped<BattleService>();
            services.AddScoped<IBattleService>(provider => new BattleCacheService(provider.GetRequiredService<BattleService>()));

            services.AddScoped<IBattleSimulatorService, BattleSimulatorService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
