using System.Collections.Generic;
using System.Threading.Tasks;
using BattleSimulator.Domain;
using BattleSimulator.Domain.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BattleSimulator.Infrastructure.DataAccess.Repositories
{
    public class BattleRepository : GenericRepository<Battle>, IBattleRepository
    {
        public BattleRepository(BattleSimulatorDbContext dbContext) : base(dbContext)
        {
        }

        public Task Create(Battle battle)
        {
            return CreateAsync(battle);
        }
        public Task<Battle> GetByBattleId(int id)
        {
            return DbSet
                .Include(x => x.Armies)
                .ThenInclude(x => x.Logs)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        
        public async Task<IEnumerable<Battle>> GetAllBattles()
        {
            return await GetAll().ToListAsync();
        }
    }
}
