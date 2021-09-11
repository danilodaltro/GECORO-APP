using System.Threading.Tasks;
using GECORO.Domain;

namespace GECORO.Persistence.Contracts
{
    public interface IRegraContrato
    {
        Task<RegraContrato[]> GetAllRegraContratoAsync();
        Task<RegraContrato> GetRegraContratoByVendedorAsync(int idVendedor);
    }
}