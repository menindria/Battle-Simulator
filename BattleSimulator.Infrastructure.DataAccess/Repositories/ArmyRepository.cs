using System.Linq;
using System.Threading.Tasks;
using BattleSimulator.Domain;
using BattleSimulator.Domain.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BattleSimulator.Infrastructure.DataAccess.Repositories
{
    public class ArmyRepository : GenericRepository<Army>, IArmyRepository
    {
        public ArmyRepository(BattleSimulatorDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Army> GetArmies()
        {
            return GetAll();
        }

        public Task<Army> GetArmyByIdAsync(int id)
        {
            return GetAll().FirstOrDefaultAsync(x => x.Id == id);
        }
        
        public Task AttackArmy(int id)
        {
            ///Used to prevent lost update
            return DbContext.Database.ExecuteSqlCommandAsync($"UPDATE [Army] SET NumberOfAttacks = NumberOfAttacks + 1 WHERE Id = {id}");
        }
    }
}