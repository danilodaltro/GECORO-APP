using System.Threading.Tasks;
using GECORO.Domain;

namespace GECORO.Persistence.Contracts
{
    public interface IRegraVendedorPersist
    {
        Task<RegraVendedor[]> GetAllRegraVendedorAsync();
        Task<RegraVendedor> GetRegraVendedorByVendedorAsync(int idVendedor);
        Task<Vendedor> GetVendedorByRegraAsync(int parcelasPagas, decimal salvdoDevedor);
    }
}