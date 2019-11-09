using BattleSimulator.Domain.Contracts.Repositories;
using BattleSimulator.Infrastructure.DataAccess;
using BattleSimulator.Infrastructure.DataAccess.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BattleSimulator.Api.Modules
{
    public static class DataAccessModule
    {
        public static void MigrateDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<BattleSimulatorDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        public static void RegisterDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BattleSimulatorDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IBattleLogRepository, BattleLogRepository>();
            services.AddScoped<IBattleRepository, BattleRepository>();
            services.AddScoped<IArmyRepository, ArmyRepository>();
        }
    }
}
