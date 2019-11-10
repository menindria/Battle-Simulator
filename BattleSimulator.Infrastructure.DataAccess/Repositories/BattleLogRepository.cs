using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleSimulator.Domain;
using BattleSimulator.Domain.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BattleSimulator.Infrastructure.DataAccess.Repositories
{
    public class BattleLogRepository : GenericRepository<Log>, IBattleLogRepository
    {
        public BattleLogRepository(BattleSimulatorDbContext dbContext) : base(dbContext)
        {
        }

        public Task Add(Log log)
        {
            return CreateAsync(log);
        }
        
        public async Task AddLogThreadSafe(Log log)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BattleSimulatorDbContext>();
            optionsBuilder.UseSqlServer(DbContext.Database.GetDbConnection().ConnectionString);
            using (var context = new BattleSimulatorDbContext(optionsBuilder.Options))
            {
                context.Add(log);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyCollection<Log>> GetLogs(int battleId)
        {
            return await GetAll().Where(x => x.Battle.Id == battleId).ToListAsync();
        }
    }
}
