using BattleSimulator.Infrastructure.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BattleSimulator.Infrastructure.DataAccess
{
    public class BattleSimulatorDbContext : DbContext
    {
        public BattleSimulatorDbContext(DbContextOptions<BattleSimulatorDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BattleConfiguration());
            modelBuilder.ApplyConfiguration(new ArmyConfiguration());
            modelBuilder.ApplyConfiguration(new LogConfiguration());
        }
    }
}
