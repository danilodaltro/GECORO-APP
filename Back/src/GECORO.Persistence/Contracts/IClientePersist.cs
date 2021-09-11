using System.Threading.Tasks;
using GECORO.Domain;

namespace GECORO.Persistence.Contracts
{
    public interface IClientePersist
    {
        Task<Cliente[]> GetAllClientesAsync();
        Task<Cliente[]> GetClientesByNomeAsync(string nome);
        Task<Cliente> GetClienteByCPFAsync(string CPF);
        Task<Cliente> GetClienteByIdAsync(int id);
    }
}