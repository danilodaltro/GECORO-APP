using System.Threading.Tasks;
using GECORO.Domain;

namespace GECORO.Persistence.Contracts
{
    public interface IVendedorPersist
    {
        Task<Vendedor[]> GetAllVendedoresAsync(bool incluiClientes = false);
        Task<Vendedor[]> GetAllVendedoresByNomeAsync(string nome, bool incluiClientes = false);
        Task<Vendedor> GetVendedorByCodigoAsync(string codigo, bool incluiClientes = false);
        Task<Vendedor> GetVendedorByIdAsync(int id, bool incluiClientes = false);
        Task<Vendedor> GetVendedorByRegraAsync(int parcelasPagas, decimal saldoDevedor);
    }
}