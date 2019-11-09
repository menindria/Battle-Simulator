using System.Threading.Tasks;

namespace BattleSimulator.Application.Contracts.Services
{
    public interface IUnitOfWork
    {
        Task SaveChanges();
    }
}