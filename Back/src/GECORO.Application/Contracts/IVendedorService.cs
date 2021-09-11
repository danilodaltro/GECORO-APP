using System.Threading.Tasks;
using GECORO.Domain;

namespace GECORO.Application.Contracts
{
    public interface IVendedorService
    {
        public Task<Vendedor> AddVendedor(Vendedor model);
        public Task<Vendedor> UpdateVendedor(int vendedorId, Vendedor model);
        public Task<bool> DeleteVendedor(int vendedorId);
        Task<Vendedor[]> GetAllVendedoresAsync(bool incluiClientes);
        Task<Vendedor[]> GetAllVendedoresByNomeAsync(string nome, bool incluiClientes);
        Task<Vendedor> GetVendedorByCodigoAsync(string codigo, bool incluiClientes);
        Task<Vendedor> GetVendedorByIdAsync(int vendedorId, bool incluiClientes);
    }

}