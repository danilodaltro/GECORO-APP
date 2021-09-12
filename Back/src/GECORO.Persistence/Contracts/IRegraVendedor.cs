using System.Threading.Tasks;
using GECORO.Domain;

namespace GECORO.Persistence.Contracts
{
    public interface IRegraVendedor
    {
        Task<RegraVendedor[]> GetAllRegraVendedorAsync();
        Task<RegraVendedor> GetRegraVendedorByVendedorAsync(int idVendedor);
    }
}