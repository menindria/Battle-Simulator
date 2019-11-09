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

        public async Task<IReadOnlyCollection<Log>> GetLogs(int battleId)
        {
            return await GetAll().Where(x => x.Battle.Id == battleId).ToListAsync();
        }
        
        public Task<Log> GetLastLogForArmy(int army)
        {
            return GetAll().OrderByDescending(x => x.TimeStamp).FirstOrDefaultAsync();
        }
    }
}
