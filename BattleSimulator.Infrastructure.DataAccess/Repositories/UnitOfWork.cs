using System.Threading.Tasks;
using BattleSimulator.Application.Contracts.Services;

namespace BattleSimulator.Infrastructure.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BattleSimulatorDbContext _dbContext;

        public UnitOfWork(BattleSimulatorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
