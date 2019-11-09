using System.Linq;
using System.Threading.Tasks;
using BattleSimulator.Domain;

namespace BattleSimulator.Infrastructure.DataAccess.Repositories.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> GetById(int id);
        Task CreateAsync(TEntity entity);
        IQueryable<TEntity> GetAll();
    }
}