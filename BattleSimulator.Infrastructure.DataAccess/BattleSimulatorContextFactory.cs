using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BattleSimulator.Infrastructure.DataAccess
{
    public class BattleSimulatorContextFactory : IDesignTimeDbContextFactory<BattleSimulatorDbContext>
    {
        public BattleSimulatorDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BattleSimulatorDbContext>();
            optionsBuilder.UseSqlServer(@"Server=.;Database=BattleSimulator;Trusted_Connection=True;MultipleActiveResultSets=true",
                opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds));
            return new BattleSimulatorDbContext(optionsBuilder.Options);
        }
    }
}