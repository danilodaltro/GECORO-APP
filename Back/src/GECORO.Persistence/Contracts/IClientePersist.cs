using System.Threading.Tasks;
using GECORO.Domain;

namespace GECORO.Persistence.Contracts
{
    public interface IClientePersist
    {
        Task<Cliente[]> GetAllClientesAsync();
        Task<Cliente[]> GetAllClientesByVendedor(int vendedorId);
        Task<Cliente> GetClienteByIdAsync(int id);
        Task<Cliente> GetClienteByCPFAsync(string cpf);
    }
}