using System.Linq;
using System.Threading.Tasks;
using BattleSimulator.Domain;
using BattleSimulator.Infrastructure.DataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BattleSimulator.Infrastructure.DataAccess.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : EntityBase
    {
        public BattleSimulatorDbContext DbContext { get; }
        protected DbSet<TEntity> DbSet { get; set; }

        public GenericRepository(BattleSimulatorDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<TEntity>();
        }

        public Task<TEntity> GetById(int id) { return DbSet.FirstOrDefaultAsync(x => x.Id == id);}
        public Task CreateAsync(TEntity entity) { return DbSet.AddAsync(entity); }
        public IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }
    }
}
